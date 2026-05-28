using System.Configuration;
using System.Data;
using System.IO;
using System.Windows;
using HotelData.DataBase;
using HotelData.RoomMan;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using SqliteData;
namespace CheckInWpf
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
            ser.AddTransient<IDapAcasses,DapAcasses>();
         //   ser.AddTransient<>
            ser.AddTransient<IdataDb, Sqldata>();
            ser.AddTransient<IDataAcsesLite, DataAcsesLite>();


            ser.AddTransient<MainWindow>();
            ser.AddTransient<CheckInForm>();
            var bulder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json");
            IConfiguration configuration = bulder.Build();
            ser.AddSingleton(configuration);
            string dbChoice =configuration.GetValue<string>("DbChoice").ToLower();
            if (dbChoice == "sql")
            {
                ser.AddTransient<IdataDb, Sqldata>();
            }
            else if (dbChoice == "sqlite")
            {
               ser.AddTransient<IdataDb, LiteDbData>();
            }
            else
            {
               ser.AddTransient<IdataDb, Sqldata>();
            }
          
            serviceProvider =ser.BuildServiceProvider();
            var mw=serviceProvider.GetService<MainWindow>();
            MainWindow.Show();
        }
    }

}
