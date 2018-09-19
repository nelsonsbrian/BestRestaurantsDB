
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using BestRestaurants.Models;
namespace BestRestaurants.Controllers
{
    public class RestaurantsController:Controller{
        [HttpGet("/restaurants")]
        public ActionResult Index()
        {
            return View();
        }
    }
}