using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelDataA.Models;
using HotelDataA.RoomMan;

namespace SqliteData
{
    public class LiteDbData : IDataDb
    {
        private readonly IDataAccessLite _db;

        public LiteDbData( IDataAccessLite db)
        {
            _db=db;
        }
        string cName = "SqliteDB";
        public void bookRoom(string firstName, string lastName, string RoomName, DateTime startDate, DateTime endDate)
        {
            var guest = new Guest() { FirstName = firstName, LastName = lastName };

            string sql = @"

insert into Gusest (FirstName,LastName) values (@firstName,@lastName);";
            _db.Save(sql, new { firstName, lastName }, cName);
             sql = "select Id from gusest where FirstName=@firstName and LastName=@lastName";
            guest.Id = _db.LoadData<Guest, dynamic>(sql, new { firstName = firstName, lastName = lastName }, cName).FirstOrDefault().Id;
            sql = "select * from RoomType t where t.name=@RoomName;";
            var type = _db.LoadData<RoomType, dynamic>(sql, new { RoomName }, cName).ToList().First();
            TimeSpan timeSpan = endDate.Date.Subtract(startDate.Date);



            sql = @"select  r .*
          from Room r
           inner join RoomType t on t.Id=r.Typeid 
          where r.TypeId=@Typeid 
          and  r.id not in(select b.RoomId from bookings b where (@startDate<b.startDate and @endDate > b.Enddate) 
or(b.Startdate<=@endDate and @endDate < b.EndDate)
or(b.StartDate<=@startDate and @endDate < b.EndDate));";
            var room = _db.LoadData<Room, dynamic>(sql, new
            {
                startDate = startDate,
                endDate
                ,
                Typeid = type.Id
            }, cName).FirstOrDefault();

            var booking = new Bookings()
            {
                GuestId = guest.Id,
                RoomId = room.ID,
                RoomNumber = room.RoomNumber,
                StartDate = startDate,
                EndDate = endDate,

                Price = timeSpan.Days * type.Price,
            };


            _db.Save("insert  into Bookings (startDate,endDate,RoomId,totelPrice,guestId) values (@startDate,@endDate,@roomId,@totelPrice,@guestid)",
                     new { startDate = booking.StartDate, endDate = booking.EndDate,roomId= booking.RoomId, totelPrice = booking.Price, guestid = booking.GuestId },
                     cName);
        }

        public void CheckIn(int bookingId)
        {


            string sql = "Update dbo.bookings set isChekedin=1 where  Id=@id; ";
            _db.Save(sql, new { id = bookingId }, cName);
        }
        

        public List<RoomType> GetAvailableRoomType(DateTime startDate, DateTime endDate)
        {
            string sql = @"select  t.Id,t.name, t.descreptin, t.Price
                          from Room r  
                          inner join RoomType t on t.Id=r.Typeid
                            where r.id not in(
                          select b.RoomId
                          from bookings b
                            where (@startDate<b.startDate and @endDate>b.Enddate)
or(b.Startdate<=@endDate and @endDate<b.EndDate)
or(b.StartDate<=@startDate and @startDate<b.EndDate)

)
group by  t.Id,t.name, t.descreptin, t.Price
";
            var output=_db.LoadData<RoomType, dynamic>(sql, new { startDate = startDate, endDate }, cName);
            output.ForEach(x => x.Price = x.Price/ 10000);
            
                
            return output;
        }


        public List<Bookings> GetBookings(string lastName)

        {
            string sql = @"	select b.endDate, b.endDate,b.guestId, b.Id ,b.RoomId,b.totelPrice ,t.name,t.descreptin,t.Price, g.FirstName,g.LastName

	from Bookings b
	inner join Gusest g on g.Id=b.guestId
	inner join Room r on r.Id=b.RoomId
	inner join RoomType t on t.Id=r.TypeId
	where g.LastName=@lastName and b.startDate=@Today";
	//group by b.id,b.guestId ,g.FirstName,g.LastName,b.RoomId,t.Price,t.name,t.descreptin,b.startDate,b.endDate,b.totelPrice";
           var bookings = _db.LoadData<Bookings, dynamic>(sql, new
            {
                lastName,
                Today = DateTime.Now,
            }, cName);
            return bookings;
        }
        
        

        public RoomType GetRoomType(string name)
        {
            throw new NotImplementedException();
        }
    }
}
