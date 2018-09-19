
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
          
            cmd.Parameters.AddWithValue("@NewName",this.Name);
             
            cmd.Parameters.AddWithValue("@CuisineId",this.CuisineId);

            cmd.ExecuteNonQuery();
            Id = (int) cmd.LastInsertedId;
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }

        public static List<Restaurant> GetAll()
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
            cmd.CommandText = @"UPDATE `restaurants` SET `name` = @NewName WHERE id = @thisId;";
           
            cmd.Parameters.AddWithValue("@NewName",newName);
          
            cmd.Parameters.AddWithValue("@thisId",this.Id);
            this.Name = newName;
            cmd.ExecuteNonQuery();

            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }

    }
}