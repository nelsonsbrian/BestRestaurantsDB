using BestRestaurants.Models;
using MySql.Data.MySqlClient;
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
            cmd.CommandText = @"INSERT INTO `cuisine` (`food`,`restaurant_id`) VALUES (@NewFood, @NewRestaurantId);";
            MySqlParameter food = new MySqlParameter();
            food.Parameters.AddWithValue("@NewFood",this.FoodType);
            MySqlParameter restaurant = new MySqlParameter();
            restaurant.Parameters.AddWithValue("@NewRestaurantId",this.RestaurantId);

            cmd.ExecuteNonQuery();
            Id = (int) cmd.LastInsertedId;
            conn.Close();
            if (conn ! =null)
            {
                conn.Dispose();
            }
        }
    }
}