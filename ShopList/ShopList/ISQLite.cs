using System;
//using SQLite.Net;
using SQLitePCL;
using SQLite;
namespace ShopList
{
    public interface ISQLite
    {
        SQLiteConnection GetConnection();
    }
}
