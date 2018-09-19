using System;
using System.Collections.Generic;
using BestRestaurants.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MySql.Data.MySqlClient;
using static BestRestaurants.Startup;

namespace BestRestaurants.Tests 
{
    [TestClass]
    public class CuisineTests : IDisposable
    {
       
        public CuisineTests()
        {
            DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=best_restaurants_test;";
        }
        [TestMethod]
        public void GetAll_CuisinesEmptyAtFirst_0()
        {
            int result = Cuisine.GetAll().Count;
            Assert.AreEqual(0,result);
        }
        [TestMethod]
        public void Create_CuisineAddedCorrectly_1()
        {
            Cuisine newfood = new Cuisine("Mexican", 2);
            newfood.Create();

            int result = Cuisine.GetAll().Count;

            Cuisine test = Cuisine.GetAll()[0];
            Assert.AreEqual(newfood.FoodType, test.FoodType);

        }
         public void Dispose()
        {  
          Cuisine.ClearAll();
          
        }
    }
}