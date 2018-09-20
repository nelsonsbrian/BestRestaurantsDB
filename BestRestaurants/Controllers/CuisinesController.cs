using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using BestRestaurants.Models;
namespace BestRestaurants.Controllers
{
    public class CuisinesController:Controller{
        [HttpGet("/cuisines")]
        public ActionResult Index()
        {
            List<Cuisine> cuisineList = Cuisine.GetAll();
            return View(cuisineList);
        }
        [HttpPost("/cuisines")]
        public ActionResult CreateCuisine()
        {
            Cuisine newCuisine = new Cuisine(Request.Form["newCuisine"]);
            newCuisine.Create();
            List<Cuisine> cuisineList = Cuisine.GetAll();
            return RedirectToAction("Index","Restaurants",cuisineList);
        }
    }
}