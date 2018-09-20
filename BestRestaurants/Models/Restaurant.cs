
using MySql.Data.MySqlClient;
using System.Collections.Generic;

namespace BestRestaurants.Models
{
    public class Restaurant
    {
        public int Id {get;set;}
        public string Name {get;set;}
        public int CuisineId {get;}
        public string City {get; set;}
        public string State {get; set;}
        public Restaurant(string newName, int newCuisineId, int newId =0)
        {
            Name = newName;
            CuisineId = newCuisineId;
            Id = newId;
        }
        public Restaurant(string newName, int newCuisineId, string newCity, string newState, int newId =0)
        {
            Name = newName;
            CuisineId = newCuisineId;
            Id = newId;
            City = newCity;            
            State = newState;
        }        
        public void Create()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();

            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = @"INSERT INTO `restaurants` (`name`,`cuisine_id`, `state`, `city`) VALUES (@NewName,@CuisineId,@state,@city);";
            cmd.Parameters.AddWithValue("@NewName",this.Name);
            cmd.Parameters.AddWithValue("@CuisineId",this.CuisineId);
            cmd.Parameters.AddWithValue("@state",this.State);
            cmd.Parameters.AddWithValue("@city",this.City);            
            cmd.ExecuteNonQuery();

            Id = (int) cmd.LastInsertedId;

            cmd.CommandText = @"INSERT INTO `menu` (`restaurant_id`,`cuisine_id`) VALUES (@RestaurantId,@CuisineId);";
            cmd.Parameters.AddWithValue("@RestaurantId",Id);
            cmd.Parameters.AddWithValue("@CuisineId",this.CuisineId);
            cmd.ExecuteNonQuery();

            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }

        public static void Remove(int idToRemove)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();

            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = @"DELETE FROM restaurants WHERE id = @deleteId;";
            cmd.Parameters.AddWithValue("@deleteId", idToRemove);
            cmd.ExecuteNonQuery();

            cmd.CommandText = @"DELETE FROM menu WHERE id = @deleteId;";
            cmd.Parameters.AddWithValue("@deleteId", idToRemove);
            cmd.ExecuteNonQuery();


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
                string State = rdr.GetString(3);
                string City = rdr.GetString(4);

                Restaurant newRestaurant = new Restaurant(Name,CuisineId,City,State,Id);
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