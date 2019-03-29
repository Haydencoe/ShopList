using System;
using SQLite;

namespace ShopList
{
    public class Data
    {

        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public DateTime CreatedOn { get; set; }
        public int GamesPlayed { get; set; }
        public int HighscoresViewed { get; set; }
        public int TrophiesViewed { get; set; }
        public int RoundCountHigh { get; set;  }

        public Data()
        {
        }
    }
}
