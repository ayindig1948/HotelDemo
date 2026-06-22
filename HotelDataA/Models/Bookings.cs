using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelDataA.Models
{
   public  class Bookings
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int RoomId {  get; set; }
        public string  RoomNumber  { get; set; }
        public decimal totelPrice {  get; set; }
        public string name {  get; set; }
        public string description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int GuestId {  get; set; }
        public Decimal Price {  get; set; } 

    }
}
