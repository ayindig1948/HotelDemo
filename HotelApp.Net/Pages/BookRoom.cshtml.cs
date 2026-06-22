using HotelDataA.Models;
using HotelDataA.RoomMan;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HotelApp.Net.Pages;

public class BookRoomModel : PageModel
{
    private readonly IDataDb _db;

    [BindProperty(SupportsGet =true)]
    public string roomtypeName { get; set; }
    [BindProperty(SupportsGet =true)]
    public int RoomtypeId { get; set; }
    [BindProperty(SupportsGet =true)]
    public DateTime StartDate { get; set; }
    [BindProperty(SupportsGet =true)]
    public DateTime EndDate { get; set; }
    [BindProperty]
    public string FirstName { get; set; }
    [BindProperty]
    public string LastName { get; set; }
    public RoomType RoomType { get; set; }
    public BookRoomModel(IDataDb db)
    {
        _db=db;
    }
    public void OnGet()
    {
      //  RoomType=_db.GetRoomType(roomtypeName);
    }
    public IActionResult OnPost() {
        _db.bookRoom(FirstName, LastName,roomtypeName,StartDate,EndDate);
        return RedirectToPage("index");
}
}