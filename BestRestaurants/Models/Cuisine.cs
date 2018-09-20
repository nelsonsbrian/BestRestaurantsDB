using System;
using System.Collections.Generic;
using BestRestaurants;
using MySql.Data.MySqlClient;

namespace BestRestaurants.Models
{
    public class Cuisine
    {
        public int Id { get; set; }
        public string FoodType { get; set; }

        public Cuisine(string newFoodType, int newId = 0)
        {
            FoodType = newFoodType;
            Id = newId;
        }
        public void Create()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();

            var cmd = conn.CreateCommand()as MySqlCommand;
            cmd.CommandText = @"INSERT INTO `cuisines` (`food`) VALUES (@NewFood);";
            MySqlParameter food = new MySqlParameter();
            cmd.Parameters.AddWithValue("@NewFood", this.FoodType);

            cmd.ExecuteNonQuery();
            Id = (int)cmd.LastInsertedId;
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }

        public static List<Cuisine> GetAll()
        {
            List<Cuisine> allCuisines = new List<Cuisine> { };
            MySqlConnection conn = DB.Connection();
            conn.Open();

            var cmd = conn.CreateCommand()as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM `cuisines`;";
            MySqlDataReader rdr = cmd.ExecuteReader()as MySqlDataReader;

            while (rdr.Read())
            {
                int Id = rdr.GetInt32(0);
                string FoodType = rdr.GetString(1);

                Cuisine newCuisine = new Cuisine(FoodType, Id);
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

            var cmd = conn.CreateCommand()as MySqlCommand;
            cmd.CommandText = @"DELETE FROM `cuisines`;";

            cmd.ExecuteNonQuery();

            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }

        public void Update(string newFoodType)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();

            var cmd = conn.CreateCommand()as MySqlCommand;
            cmd.CommandText = @"UPDATE `cuisines` SET food = @NewType WHERE id = @thisId;";

            MySqlParameter foodType = new MySqlParameter();
            cmd.Parameters.AddWithValue("@NewType", newFoodType);
            MySqlParameter Id = new MySqlParameter();
            cmd.Parameters.AddWithValue("@thisId", this.Id);
            this.FoodType = newFoodType;
            cmd.ExecuteNonQuery();

            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }

        public static int FindId(string cuisineName)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();

            var cmd = conn.CreateCommand()as MySqlCommand;
            cmd.CommandText = @"SELECT id FROM `cuisines` WHERE food = @NewType;";

            cmd.Parameters.AddWithValue("@NewType", cuisineName);
            MySqlDataReader rdr = cmd.ExecuteReader()as MySqlDataReader;

            rdr.Read();
            int newId = rdr.GetInt32(0);

            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
            return newId;
        }
        
    }
}