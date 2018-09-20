using System;
using System.Collections.Generic;
using BestRestaurants;
using MySql.Data.MySqlClient;

namespace BestRestaurants.Models
{
    public class Menu
    {
        public int Id { get; set; }
        public int CuisineId { get; set; }
        public int RestaurantId { get; set; }

        public Menu(int newCuisineId, int newRestaurantId, int Id = 0)
        {
            CuisineId = newCuisineId;
            RestaurantId = newRestaurantId;
        }
        public static List<Menu> GetAll()
        {
            List<Menu> allMenus = new List<Menu> {};
            MySqlConnection conn = DB.Connection();
            conn.Open();

            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM `menu`;";
            MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;

            while (rdr.Read())
            {
                int Id = rdr.GetInt32(0);
                int RestaurantId = rdr.GetInt32(1);
                int CuisineId = rdr.GetInt32(2);
               
                Menu newMenu = new Menu(CuisineId,RestaurantId);
                allMenus.Add(newMenu);
            }
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
            return allMenus;
        }

        public void Create()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();

            var cmd = conn.CreateCommand()as MySqlCommand;
            cmd.CommandText = @"INSERT INTO `menu` (`restaurant_id`, `cuisine_id`) VALUES (@Rest_id, @Cuis_id);";
            MySqlParameter food = new MySqlParameter();
            cmd.Parameters.AddWithValue("@Rest_id", this.RestaurantId);
            cmd.Parameters.AddWithValue("@Cuis_id", this.CuisineId);
            cmd.ExecuteNonQuery();
            Id = (int)cmd.LastInsertedId;
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }        

    }
}