using BestRestaurants.Models;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
namespace BestRestaurants
{
    public class Restaurant
    {
        public int Id {get;set;}
        public string Name {get;set;}

        public Restaurant(string newName, int newId =0)
        {
            Name = newName;
            Id = newId;
        }
        public void Create()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();

            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"INSERT INTO `restaurants` (`name`) VALUES (@NewName);";
            MySqlParameter name = new MySqlParameter();
            cmd.Parameters.AddWithValue("@NewName",this.Name);
            

            cmd.ExecuteNonQuery();
            Id = (int) cmd.LastInsertedId;
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }

        public List<Restaurant> GetAll()
        {
            List<Restaurant> allRestaurants = new List<Restaurant> {};
            MySqlConnection conn = DB.Connection();
            conn.Open();

            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM `restaurants`;";
            MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;

            while (rdr.Read())
            {
                int Id = rdr.GetInt32(0);
                string Name = rdr.GetString(1);

                Restaurant newRestaurant = new Restaurant(Name, Id);
                allRestaurants.Add(newRestaurant);
            }
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
            return allRestaurants;
        }

        public static void ClearAll()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();

            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"DELETE FROM `restaurants`;";

            cmd.ExecuteNonQuery();

            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }


    }
}