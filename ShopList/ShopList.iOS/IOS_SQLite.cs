
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xamarin.Forms;
//using SQLite.Net;
using SQLite;
using ShopList.iOS;
using SQLitePCL;


[assembly: Dependency(typeof(IOS_SQLite))]

namespace ShopList.iOS
{

    public class IOS_SQLite : ISQLite
      {
        public SQLiteConnection GetConnection()
        {
            var dbName = "ShopList.sqlite";
            string dbPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal); // Documents folder  
            string libraryPath = Path.Combine(dbPath, "..", "Library"); // Library folder  
            var path = Path.Combine(libraryPath, dbName);

            // var iOSPlatform = new SQLite..XamarinIOS.SQLitePlatformIOS();
            //var iOSPlatform = Device.iOS;

            var conn = new SQLiteConnection(path, true);

            //return conn;
            return conn;
        }
    }


}