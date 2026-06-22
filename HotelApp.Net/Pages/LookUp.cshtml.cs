using System.ComponentModel.DataAnnotations;
using HotelDataA.Models;
using HotelDataA.RoomMan;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HotelApp.Net.Pages
{
    public class LookUpModel : PageModel
    {
        private readonly IDataDb _db;

        [DataType(DataType.Date)]
        [BindProperty(SupportsGet =true)]
        public DateTime  StartDate { get; set; }= DateTime.Now;
        [BindProperty(SupportsGet =true)]

        [DataType(DataType.Date)]
       public DateTime EndDate { get; set; }=DateTime.Now.AddDays(1);
        [BindProperty(SupportsGet =true)]
        public bool SearchIsE {  get; set; }=false;
        public List<RoomType> RoomTypes { get; set; }
        public LookUpModel(IDataDb db)
        {
            _db=db;
        }
        public void OnGet()
        {
            if (SearchIsE==true)
          {
             RoomTypes=_db.GetAvailableRoomType(StartDate, EndDate);

           }

        }
        public IActionResult OnPost() {
        
        return RedirectToPage(new {SearchIsE=true,
            StartDate=StartDate.ToString("yyyy-MM-dd"),EndDate
        =EndDate.ToString("yyyy-MM-dd")});
       
        }    
    }
}
