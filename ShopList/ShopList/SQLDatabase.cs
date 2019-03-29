using System;
using SQLite;
using Xamarin.Forms;
using System.Collections.Generic;
using System.Linq;

namespace ShopList
{
    public class SQLDatabase
    {
        private SQLiteConnection connection;

        // SQLite Connection Refrence.
        public SQLDatabase()
        {
            connection = DependencyService.Get<ISQLite>().GetConnection();

            // Create Hard highscore table.
            connection.CreateTable<HardHighscore>();

            // Create Medium highscore table.
            connection.CreateTable<MediumHighscore>();

            // Create Easy highscore table.
            connection.CreateTable<EasyHighscore>();

            // Create Trophy table.
            connection.CreateTable<Trophies>();

            // Create Data table.
            connection.CreateTable<Data>();

        }


        //************************************************************
        // Hard highscores table methods.

        // Get all the Hard highscores.
        public List<HardHighscore> GetAllHardHighscores()
        {
            return (from t in connection.Table<HardHighscore>()
                    select t).ToList();
        }

        // Get an individual Hard highscore.
        public HardHighscore GetHardHighscore(int id)
        {
            return connection.Table<HardHighscore>().FirstOrDefault(t => t.ID == id);
        }


        // Delete all the Hard highscores Data.  
        public void DeleteAllHardHighscores()
        {
            connection.DeleteAll<HardHighscore>();
        }


        // Delete an individual Hard highscore.
        public void DeleteHighscore(int id)
        {
            connection.Delete<HardHighscore>(id);
        }

        // Update Hard highscore Data.  
        public void UpdateHardHighscore(HardHighscore hardHighscore)
        {
            connection.Update(hardHighscore);
        }

        // Add a new Hard highscore entry.
        public void AddHardHighscore(HardHighscore score)
        {
            /*
            var newHardHighscore = new HardHighscore
            {
                Round = round,
                Name = name,
                CreatedOn = DateTime.Now
            };
            */

            connection.Insert(score);
        }

        //************************************************************
        // Medium highscores table methods.

        // Get all the Medium highscores.
        public List<MediumHighscore> GetAllMediumHighscores()
        {
            return (from t in connection.Table<MediumHighscore>()
                    select t).ToList();
        }

        // Get an individual Medium highscore.
        public MediumHighscore GetMediumHighscore(int id)
        {
            return connection.Table<MediumHighscore>().FirstOrDefault(t => t.ID == id);
        }


        // Delete all the Medium highscores Data.  
        public void DeleteAllMediumHighscores()
        {
            connection.DeleteAll<MediumHighscore>();
        }


        // Delete an individual Medium highscore.
        public void DeleteMediumHighscore(int id)
        {
            connection.Delete<MediumHighscore>(id);
        }

        // Update Medium highscore Data.  
        public void UpdateMediumHighscore(MediumHighscore mediumHighscore)
        {
            connection.Update(mediumHighscore);
        }

        // Add a new Medium highscore entry.
        public void AddMediumHighscore(MediumHighscore score)
        {

            connection.Insert(score);
        }

        //************************************************************
        // Easy highscores table methods.

        // Get all the Easy highscores.
        public List<EasyHighscore> GetAllEasyHighscores()
        {
            return (from t in connection.Table<EasyHighscore>()
                    select t).ToList();
        }

        // Get an individual Easy highscore.
        public EasyHighscore GetEasyHighscore(int id)
        {
            return connection.Table<EasyHighscore>().FirstOrDefault(t => t.ID == id);
        }


        // Delete all the Easy highscores Data.  
        public void DeleteAllEasyHighscores()
        {
            connection.DeleteAll<EasyHighscore>();
        }


        // Delete an individual Easy highscore.
        public void DeleteEasyHighscore(int id)
        {
            connection.Delete<EasyHighscore>(id);
        }

        // Update Easy highscore Data.  
        public void UpdateEasyHighscore(EasyHighscore easyHighscore)
        {
            connection.Update(easyHighscore);
        }

        // Add a new Easy highscore entry.
        public void AddEasyHighscore(EasyHighscore score)
        {

            connection.Insert(score);
        }

        //************************************************************
        // Trophies table methods.

        // Get all the trophies.
        public List<Trophies> GetAllTrophies()
        {
            return (from t in connection.Table<Trophies>()
                    select t).ToList();
        }

        // Delete all the trophies.  
        public void DeleteAllTrophies()
        {
            connection.DeleteAll<Trophies>();
        }

        // Add a new trophy.
        public void AddTrophy(Trophies trophy)
        {
            connection.Insert(trophy);
        }

        //************************************************************
        // Game data table methods.

        public List<Data> GetAllData()
        {
            return (from t in connection.Table<Data>()
                    select t).ToList();
        }

        // Delete all the trophies.  
        public void DeleteAllData()
        {
            connection.DeleteAll<Data>();
        }

        // Add a new trophy.
        public void AddData(Data data)
        {
            connection.Insert(data);
        }

        // Update Contact Data  
        public void UpdateData(Data data)
        {
            connection.Update(data);
        }

    } // End of class

} // End of namespace
