using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelDataA.DataBase;
using HotelDataA.Models;

namespace HotelDataA.RoomMan;

public class Sqldata : IDataDb
{
    private readonly IDapAccess _db;

    public Sqldata(IDapAccess db)
    {
        _db = db;
    }
    private const string cName = "SqlDb";
    public List<RoomType> GetAvailableRoomType(DateTime startDate, DateTime endDate)
    {
        return _db.LoadData<RoomType, dynamic>("spBookigs_GetAvlebeleRooms", new {startDate= startDate, endDate }, cName, true);
    }
    public RoomType GetRoomType(string name)
    {
     var t=   _db.LoadData<RoomType,dynamic>("spRoomType_GetByName", new {name},cName, true).FirstOrDefault();
        return t;
    }
    public void bookRoom(string firstName, string lastName, string RoomName, DateTime startDate, DateTime endDate)
    {
        var guest = new Guest() { FirstName = firstName, LastName = lastName };

        _db.Save("spGusest_Creat", new {  firstName, lastName }, cName,true);
           string  sql = "select Id from dbo.gusest where FirstName=@firstName and LastName=@lastName";
        guest.Id = _db.LoadData<Guest, dynamic>(sql, new { firstName= firstName,lastName= lastName }, cName).FirstOrDefault().Id;
        sql = "select* from dbo.RoomType t where t.name=@RoomName;";
        var type = _db.LoadData<RoomType, dynamic>(sql, new {  RoomName }, cName).ToList().First();
      TimeSpan timeSpan       = endDate.Date.Subtract(startDate.Date);

         


        var room = _db.LoadData<Room, dynamic>("spRoom_getRoom", new { startDate=startDate,endDate
            , Typeid = type.Id }, cName, true).FirstOrDefault();

        var booking = new Bookings()
        {
            GuestId = guest.Id,
            RoomId = room.ID,
            RoomNumber = room.RoomNumber,
            StartDate = startDate,
            EndDate = endDate,
           
           Price = timeSpan.Days*type.Price,
        };


        _db.Save("spBooking-book",
                 new { startDate = booking.StartDate, EndDate = booking.EndDate, booking.RoomId, booking.RoomNumber, totelPrice = booking.Price, guestId = booking.GuestId },
                 cName,
                 true);
    }
    public List<Bookings> GetBookings(string lastName)
    {

        
        
        
        
        
        var bookings = _db.LoadData<Bookings, dynamic>("spBooking_GetBookings_", new
        {
            lastName,
            Today =DateTime.Now,
        }, cName, true);
        return bookings;
    }
    public void CheckIn(int bookingId)
    {

        string sql = "Update dbo.bookings set isChekedin=1 where  Id=@id; ";
        _db.Save(sql, new { id = bookingId }, cName, true);
    }
}
       


    
    
