using System;
using SQLite;

namespace ShopList
{
    public class Trophies
    {

        [PrimaryKey, AutoIncrement] 
        public int ID { get; set; }
        public string Name { get; set; }
        public string Trophy { get; set; }
        public string TrophyPic { get; set;  }
        public DateTime CreatedOn { get; set; }

        public Trophies()
        {
        }
    }
}
