using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
namespace DataGenerator
{
    class SqlDBConfig
    {
        public static string connectionString = "Server=localhost;Database=smartfit2;Uid=root;Pwd=;";
        public MySqlConnection con;
        public void connectToDB()
        {

            con = new MySqlConnection(connectionString);
            con.Open();
        }
        public void breakConnection()
        {
            con.Close();
        }

        public List<int> getUserIDs()
        {
            connectToDB();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = con;
            List<int> userIDs = new List<int>();
            string query = "SELECT ID FROM USERS;";
            cmd.CommandText = query;
            MySqlDataReader reader = cmd.ExecuteReader();

            while(reader.Read())
            {
                userIDs.Add(reader.GetInt32(0));
            }

            breakConnection();
            return userIDs;
        }

        public List<int> getExerciseIDs()
        {
            connectToDB();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = con;
            List<int> exerciseIDs = new List<int>();
            string query = "SELECT ID FROM EXERCISES;";
            cmd.CommandText = query;
            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                exerciseIDs.Add(reader.GetInt32(0));
            }
            breakConnection();
            return exerciseIDs;
        }

        public List<int> getWorkoutIDs()
        {
            connectToDB();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = con;
            List<int> workoutIDs = new List<int>();
            string query = "SELECT ID FROM WORKOUTS;";
            cmd.CommandText = query;
            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                workoutIDs.Add(reader.GetInt32(0));
            }
            breakConnection();
            return workoutIDs;
        }
    }
}
