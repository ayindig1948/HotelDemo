using HotelDataA.Models;

namespace HotelDataA.RoomMan
{
    public interface IDataDb
    {
        void bookRoom(string firstName, string lastName, string RoomName, DateTime startDate, DateTime endDate);
        void CheckIn(int bookingId);
        List<RoomType> GetAvailableRoomType(DateTime startDate, DateTime endDate);
        List<Bookings> GetBookings(string lastName);
        RoomType GetRoomType(string name);
    }
}