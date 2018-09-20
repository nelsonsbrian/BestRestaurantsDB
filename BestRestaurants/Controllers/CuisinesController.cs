using System;
using System.Collections.Generic;
using BestRestaurants.Models;
using Microsoft.AspNetCore.Mvc;
namespace BestRestaurants.Controllers
{
    public class CuisinesController : Controller
    {
        [HttpGet("/cuisines")]
        public ActionResult Index()
        {
            List<Cuisine> cuisineList = Cuisine.GetAll();
            return View(cuisineList);
        }

        [HttpPost("/cuisines")]
        public ActionResult Create()
        {
            List<Cuisine> cuisineList = Cuisine.GetAll();

            int flag = 0;
            foreach (Cuisine cuisine in cuisineList)
            {
                if (cuisine.FoodType == Request.Form["newCuisine"])
                {
                    flag++;
                }
            }
            if (flag == 0)
            {
                Cuisine newCuisine = new Cuisine(Request.Form["newCuisine"]);
                newCuisine.Create();
                return RedirectToAction("Index", "Restaurants");
            }
            return RedirectToAction("Index","Restaurants");
        }

    }
}