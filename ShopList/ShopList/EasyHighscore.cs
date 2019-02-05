using System;
using SQLite;

namespace ShopList
{
    public class EasyHighscore
    {

        [PrimaryKey]
        public int ID { get; set; }
        public string Name { get; set; }
        public string Round { get; set; }
        public DateTime CreatedOn { get; set; }

        public EasyHighscore()
        {
        }

   
    }// End of class.

}// End of namespace.
