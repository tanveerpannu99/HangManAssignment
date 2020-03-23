using System;
using System.Collections.Generic;
using System.IO;

namespace HangManAssignment
{
    public class DatabaseManager
    {
        static string dbName = "HangManWords.sqlite";
        string dbPath = Path.Combine(Xamarin.Essentials.FileSystem.AppDataDirectory, dbName);

        public DatabaseManager()
        {
        }

        //Function for reading words from the database, pass in category
        public List<Words> readWords(string category)
        {
            try
            {
                using (var conn = new SQLite.SQLiteConnection(dbPath))
                {
                    var cmd = new SQLite.SQLiteCommand(conn);
                    //Select word from given category
                    cmd.CommandText = "SELECT * FROM WORDS WHERE CATEGORY = '" + category + "'";
                    var WordList = cmd.ExecuteQuery<Words>();
                    return WordList;
                }
            }
            catch (Exception e)
            {

                Console.WriteLine("Error:" + e.Message);
                return null;
            }

        }

        //Function for executing qurey to the database, pass in query string
        public void runQuery(string query)
        {
            try
            {
                using (var conn = new SQLite.SQLiteConnection(dbPath))
                {
                    var cmd = new SQLite.SQLiteCommand(conn);
                    cmd.CommandText = query;
                    var rowcount = cmd.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error:" + e.Message);
            }

        }

        //Function for reading hi score from database
        public List<HiScores> readScores()
        {
            try
            {
                using (var conn = new SQLite.SQLiteConnection(dbPath))
                {
                    var cmd = new SQLite.SQLiteCommand(conn);
                    //Select 10 top scores in order from database
                    cmd.CommandText = "SELECT * FROM HIScores order by Score desc limit 10";
                    var HiScores = cmd.ExecuteQuery<HiScores>();
                    return HiScores;
                }
            }
            catch (Exception e)
            {

                Console.WriteLine("Error:" + e.Message);
                return null;
            }

        }

    }
}
