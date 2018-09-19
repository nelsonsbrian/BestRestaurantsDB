using BestRestaurants.Models;
using MySql.Data.MySqlClient;
namespace BestRestaurants
{
    public class Cuisine
    {
        public int id {get;}
        public string foodType {get;set;}
        public int restaurantId {get;}
        public Cuisine(string newFoodType, int newRestaurantId)
        {
            foodType = newFoodType;
            restaurantId = newRestaurantId;
        }
        public void Create()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();

            var cmd = conn.CreateCommand() as MySqlCommand;
            
        }
    }
}