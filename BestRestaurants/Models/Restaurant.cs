
using MySql.Data.MySqlClient;
using System.Collections.Generic;

namespace BestRestaurants.Models
{
    public class Restaurant
    {
        public int Id {get;set;}
        public string Name {get;set;}
        public int CuisineId {get;}
        public Restaurant(string newName, int newCuisineId, int newId =0)
        {
            Name = newName;
            CuisineId = newCuisineId;
            Id = newId;
        }
        public void Create()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();

            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"INSERT INTO `restaurants` (`name`,`cuisine_id`) VALUES (@NewName,@CuisineId);";
            MySqlParameter name = new MySqlParameter();
            cmd.Parameters.AddWithValue("@NewName",this.Name);
             MySqlParameter restaurant = new MySqlParameter();
            cmd.Parameters.AddWithValue("@NewCuisineId",this.CuisineId);

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
                int CuisineId = rdr.GetInt32(2);

                Restaurant newRestaurant = new Restaurant(Name,CuisineId, Id);
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
        public void Update (string newName)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();

            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"UPDATE `restaurants` SET `name` = '@NewName' WHERE id = @Id;";
            MySqlParameter name = new MySqlParameter();
            cmd.Parameters.AddWithValue("@NewName",newName);
            MySqlParameter Id = new MySqlParameter();
            cmd.Parameters.AddWithValue("@thisId",this.Id);

            cmd.ExecuteNonQuery();

            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }

    }
}