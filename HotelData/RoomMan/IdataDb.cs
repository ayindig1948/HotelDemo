using HotelData.Moudls;

namespace HotelData.RoomMan
{
    public interface IdataDb
    {
        void bookRoom(string firstName, string lastName, string RoomName, DateTime stratData, DateTime endDate);
        void Chakin(int bookingId);
        List<RoomType> GeAvlRoomType(DateTime stratData, DateTime endDate);
        List<Bookings> GetBookings(string lastName);
        RoomType GetRoomType(string name);
    }
}
