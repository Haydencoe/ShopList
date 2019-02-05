using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

using SQLite;
using ShopList.Droid;
using Xamarin.Forms;


[assembly: Dependency(typeof(Android_SQLite))]
namespace ShopList.Droid
{
    public class Android_SQLite : ISQLite
    {
        #region ISQLite implementation
        public SQLiteConnection GetConnection()
        {

            /*

            public SQLiteConnection GetConnection()
            {
                var sqliteFilename = "OpportunityModelSQLite.db3";
                string documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal); 
                var path = Path.Combine(documentsPath, sqliteFilename);

                var conn = new SQLiteConnection(path, SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create | SQLiteOpenFlags.SharedCache);

                // Return the database connection 
                return conn;
            }
           
            */


            var dbName = "ShopList.sqlite";
            var dbPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal); // Documents folder
            var path = System.IO.Path.Combine(dbPath, dbName);

            //var AndroidPlatform = new SQLite.Net.Platform.XamarinAndroid.SQLitePlatformAndroid();
           // var AndroidPlatform = Device.Android;

            if(!File.Exists(path))
            {
                File.Create(path);
            }


            var conn = new SQLiteConnection(path, true);
            return conn;
            

         
        }
        #endregion
    }
}