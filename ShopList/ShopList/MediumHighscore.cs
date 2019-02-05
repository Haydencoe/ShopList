using System;
using SQLite;

namespace ShopList
{
    public class MediumHighscore
    {
      
        [PrimaryKey]
        public int ID { get; set; }
        public string Name { get; set; }
        public string Round { get; set; }
        public DateTime CreatedOn { get; set; }

        public MediumHighscore()
        {
        }

    }// End of class.
}// End of namespace.
