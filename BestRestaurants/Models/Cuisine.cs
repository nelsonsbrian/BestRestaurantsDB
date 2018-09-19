using BestRestaurants.Models;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
namespace BestRestaurants
{
    public class Cuisine
    {
        public int Id {get;set;}
        public string FoodType {get;set;}
        public int RestaurantId {get;}
        public Cuisine(string newFoodType, int newRestaurantId, int newId =0)
        {
            FoodType = newFoodType;
            RestaurantId = newRestaurantId;
            Id = newId;
        }
        public void Create()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();

            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"INSERT INTO `cuisines` (`food`,`restaurant_id`) VALUES (@NewFood, @NewRestaurantId);";
            MySqlParameter food = new MySqlParameter();
            food.Parameters.AddWithValue("@NewFood",this.FoodType);
            MySqlParameter restaurant = new MySqlParameter();
            cmd.Parameters.AddWithValue("@NewRestaurantId",this.RestaurantId);

            cmd.ExecuteNonQuery();
            Id = (int) cmd.LastInsertedId;
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }

        public List<Cuisine> GetAll()
        {
            List<Cuisine> allCuisines = new List<cuisine> {};
            MySqlConnection conn = DB.Connection();
            conn.Open();

            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM `cuisines`;";
            MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;

            while (rdr.Read())
            {
                int Id = rdr.GetInt32(0);
                string FoodType = rdr.GetString(1);
                int RestaurantId = rdr.GetInt32(2);

                Cuisine newCuisine = new Cuisine(Id, FoodType, RestaurantId);
                allCuisines.Add(newCuisine);
            }
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
            return allCuisines;
        }

        public static void ClearAll()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();

            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"DELETE FROM `cuisines`;";

            cmd.ExecuteNonQuery();

            conn.Close();
            if (conn !=null)
            {
                conn.Dispose();
            }
        }


    }
}