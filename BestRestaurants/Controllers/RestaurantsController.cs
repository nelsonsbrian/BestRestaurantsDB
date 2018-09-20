using System;
using System.Collections.Generic;
using BestRestaurants.Models;
using Microsoft.AspNetCore.Mvc;
namespace BestRestaurants.Controllers
{
    public class RestaurantsController : Controller
    {
        [HttpGet("/restaurants")]
        public ActionResult Index()
        {
            Dictionary<string,object> Dict = new Dictionary<string, object>{};
            List<Cuisine> allCuisines = Cuisine.GetAll();
            List<Restaurant> allRestaurants = Restaurant.GetAll();
            Dict.Add("cuisines", allCuisines);
            Dict.Add("restaurants", allRestaurants);
            return View(Dict);
        }

        [HttpPost("/restaurants")]
        public ActionResult Create()
        {
            int cuisineId = Cuisine.FindId(Request.Form["cuisinesSelect"]);

            Restaurant newRestaurant = new Restaurant(Request.Form["newRestaurant"],cuisineId);
            newRestaurant.Create();
            return RedirectToAction("Index");
        }

        [HttpPost("/restaurants/delete/{id}")]
        public ActionResult Delete(int id)
        {
            Console.WriteLine("id" + id);
            Restaurant.Remove(id);
            Dictionary<string,object> Dict = new Dictionary<string, object>{};
            List<Cuisine> allCuisines = Cuisine.GetAll();
            List<Restaurant> allRestaurants = Restaurant.GetAll();
            Dict.Add("cuisines", allCuisines);
            Dict.Add("restaurants", allRestaurants);            
            return View("Index", Dict);
        }
    }

}