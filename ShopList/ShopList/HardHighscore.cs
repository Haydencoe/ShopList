using System;
//using SQLite.Net.Attributes;
using SQLitePCL;
using SQLite;
namespace ShopList
{
    public class HardHighscore
    {

        [PrimaryKey]
        public int ID { get; set; }
        public string Name { get; set; }
        public string Round { get; set; }
        public DateTime CreatedOn { get; set; }

        public HardHighscore()
        {
        }
    
    
    }// End of class

}// End of namespace

