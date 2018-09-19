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
        public void Dispose()
        {  
          Cuisine.ClearAll();
          Restaurant.ClearAll();
        }
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
    }
}