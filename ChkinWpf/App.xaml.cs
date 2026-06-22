using System.Configuration;
using System.Data;
using System.IO;
using System.Windows;
using HotelDataA.DataBase;
using HotelDataA.RoomMan;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using SqliteData;
namespace ChkinWpf
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static ServiceProvider serviceProvider;
        protected override void OnStartup(StartupEventArgs e)
        {
            
            base.OnStartup(e);
            var ser=new ServiceCollection();
            ser.AddTransient<IDapAccess,DapAccess>();
         //   ser.AddTransient<>
            ser.AddTransient<IDataDb, Sqldata>();
            ser.AddTransient<IDataAccessLite, DataAccessLite>();


            ser.AddTransient<MainWindow>();
            ser.AddTransient<CheckInForm>();
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json");
            IConfiguration configuration = builder.Build();
            ser.AddSingleton(configuration);
            string dbChoice =configuration.GetValue<string>("DbChoice").ToLower();
            if (dbChoice == "sql")
            {
                ser.AddTransient<IDataDb, Sqldata>();
            }
            else if (dbChoice == "sqlite")
            {
               ser.AddTransient<IDataDb, LiteDbData>();
            }
            else
            {
               ser.AddTransient<IDataDb, Sqldata>();
            }
          
            serviceProvider =ser.BuildServiceProvider();
            var mw=serviceProvider.GetService<MainWindow>();
            MainWindow.Show();
        }
    }

}
