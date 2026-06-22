using System.ComponentModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using HotelDataA.Models;
using HotelDataA.RoomMan;
using Microsoft.Extensions.DependencyInjection;
namespace ChkinWpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly IDataDb _db;
       // BindingList<Bookings> bookingsList=new BindingList<Bookings>();


        public MainWindow(IDataDb db)
        {
            InitializeComponent();
            _db=db;
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
       
        var b=   _db. GetBookings(lastname.Text);
          resultsList.ItemsSource = b;

            //  bookingsList.Add(b);


        }

        private void CheckIn_Click_1(object sender, RoutedEventArgs e)
        {
            var cIf = App.serviceProvider.GetService<CheckInForm>();
            var model =( Bookings)((Button)e.Source).DataContext;
            cIf.FillFrom(model);
            cIf.Show();
        }
    }
}