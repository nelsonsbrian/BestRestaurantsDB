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
            List<Menu> allMenus = Menu.GetAll();
            List<Cuisine> allCuisines = Cuisine.GetAll();
            List<Restaurant> allRestaurants = Restaurant.GetAll();
            Dict.Add("cuisines", allCuisines);
            Dict.Add("restaurants", allRestaurants);
            Dict.Add("menus", allMenus);
            return View(Dict);
        }

        [HttpPost("/restaurants")]
        public ActionResult Create()
        {
            int cuisineId = Cuisine.FindId(Request.Form["cuisinesSelect"]);
            List<Restaurant> RestaurantList = Restaurant.GetAll();
            int flag = 0;
            foreach (Restaurant Restaurant in RestaurantList)
            {
                if (Restaurant.Name== Request.Form["newName"])
                {
                    flag++;
                }
            }
            if (flag == 0)
            {
            Restaurant newRestaurant = new Restaurant(Request.Form["newName"],cuisineId,Request.Form["newCity"],Request.Form["newState"]);
            newRestaurant.Create();
            }
            return RedirectToAction("Index");
        }

        [HttpPost("/restaurants/delete/{id}")]
        public ActionResult Delete(int id)
        {
            Restaurant.Remove(id);
            return RedirectToAction("Index");
        }

        [HttpPost("/restaurants/{id}/Add")]
        public ActionResult AppendCuisine(int id)
        {
            int newCuisineId = Cuisine.FindId(Request.Form["cuisinesSelect"]);
            List <int> allCuisinesOfRestaurant = new List<int>{};
            List<Menu> allMenus = Menu.GetAll();
            foreach (Menu menu in allMenus)
            {
                if (menu.RestaurantId == id)
                {
                    allCuisinesOfRestaurant.Add(menu.CuisineId);
                }
            }
            if (!allCuisinesOfRestaurant.Contains(newCuisineId))
            {
                Menu newMenu = new Menu(newCuisineId, id);
                newMenu.Create();
            }
           
            return RedirectToAction("Index");
        }
    }

}