using System;
using System.Collections.Generic;
using BestRestaurants.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MySql.Data.MySqlClient;
using static BestRestaurants.Startup;

namespace BestRestaurants.Tests {
    [TestClass]
    public class CuisineTests : IDisposable {

        public CuisineTests () {
            DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=best_restaurants_test;";
        }

        public void Dispose () {
            Cuisine.ClearAll ();
        }

        [TestMethod]
        public void GetAll_CuisinesEmptyAtFirst_0 () {
            int result = Cuisine.GetAll ().Count;
            Assert.AreEqual (0, result);
        }

        [TestMethod]
        public void Create_CuisineAddedCorrectly_True () {
            Cuisine newfood = new Cuisine ("Mexican");
            newfood.Create ();

            Cuisine test = Cuisine.GetAll () [0];
            Assert.AreEqual (newfood.FoodType, test.FoodType);
        }
        
        [TestMethod]
        public void ClearAll_DeleteAllCusines_Int ()
        {
            Cuisine newfood1 = new Cuisine ("Mexican");
            newfood1.Create ();
            Cuisine newfood2 = new Cuisine ("American");
            newfood2.Create ();
            Cuisine newfood3 = new Cuisine ("Italian");
            newfood3.Create ();
            Cuisine newfood4 = new Cuisine ("Chinese");
            newfood4.Create ();
            Cuisine.ClearAll();
            Cuisine newfood5 = new Cuisine ("Vietnamese");
            newfood5.Create ();            
            int result = Cuisine.GetAll().Count;
            Assert.AreEqual( result, 1);
        }

        [TestMethod]
        public void Update_ChangeCuisineNameCorrectly_True()
        {
            Cuisine newFood = new Cuisine("Mexican");
            
            newFood.Create();
            Console.WriteLine(newFood.Id);
            newFood.Update("Fake Mexican");

            Cuisine result = Cuisine.GetAll()[0];

            Assert.AreEqual("Fake Mexican",newFood.FoodType);
        }
    }
}