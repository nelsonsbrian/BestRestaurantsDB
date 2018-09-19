using System;
using System.Collections.Generic;
using BestRestaurants.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MySql.Data.MySqlClient;
using static BestRestaurants.Startup;

namespace BestRestaurants.Tests {
    [TestClass]
    public class RestaurantTests : IDisposable {

        public RestaurantTests () {
            DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=best_restaurants_test;";
        }

        public void Dispose () {
            Cuisine.ClearAll ();
            Restaurant.ClearAll ();
        }

        [TestMethod]
        public void GetAll_RestaurantsEmptyAtFirst_0 () {
            int result = Restaurant.GetAll ().Count;
            Assert.AreEqual (0, result);
        }

        [TestMethod]
        public void Create_CuisineAddedCorrectly_True () {
            Cuisine newfood = new Cuisine ("Mexican");
            newfood.Create ();
            Restaurant newRest = new Restaurant ("Happies", newfood.Id);
            newRest.Create ();

            Assert.AreEqual ("Happies", newRest.Name);

        }
        [TestMethod]
        public void ClearAll_DeleteAllCusines_Int ()
        {
            Cuisine newFood = new Cuisine ("Mexican");
            newFood.Create ();
            Restaurant newRestaurant1 = new Restaurant ("1", newFood.Id);
            newRestaurant1.Create ();
            Restaurant newRestaurant2 = new Restaurant ("2", newFood.Id);
            newRestaurant2.Create ();
            Restaurant newRestaurant3 = new Restaurant ("3", newFood.Id);
            newRestaurant3.Create ();
            Restaurant newRestaurant4 = new Restaurant ("4", newFood.Id);
            newRestaurant4.Create ();
            Restaurant.ClearAll();
            Restaurant newfood5 = new Restaurant ("5", newFood.Id);
            newfood5.Create ();            
            int result = Restaurant.GetAll().Count;
            Assert.AreEqual(result, 1);
        }

        [TestMethod]
        public void Update_ChangeCuisineNameCorrectly_True()
        {
            Cuisine newCuisine = new Cuisine ("Italian");
            newCuisine.Create ();
            Restaurant newRestaurant = new Restaurant("Yumm!", newCuisine.Id);
            
            newRestaurant.Create();
           
            newRestaurant.Update("Gross!");

            Assert.AreEqual("Gross!",newRestaurant.Name);
        }

    }
}