using Nancy;
using System;
using System.Collections.Generic;

namespace Yelp
{
  public class HomeModule : NancyModule
  {
    public HomeModule()
    {
      Get["/"] = _ => {
        return View["index.cshtml"];
      };
      Get["/cuisines"] = _ =>{
        List<Cuisine> allCuisines = Cuisine.GetAll();
        return View["cuisines.cshtml", allCuisines];
      };
      Post["/cuisines/new"] = _ => {
        string cuisineName = Request.Form["cuisine"];
        Cuisine newCuisine = new Cuisine(cuisineName);
        newCuisine.Save();
        List<Cuisine> allCuisines = Cuisine.GetAll();
        return View["cuisines.cshtml", allCuisines];
      };
      Get["/restaurants"] = _ =>{
        List<Restaurant> allRestaurants = Restaurant.GetAll();
        return View["restaurants.cshtml", allRestaurants];
      };
      Post["/restaurants/new"] = _ =>{
        string newName = Request.Form["name"];
        string newFavDish = Request.Form["fav-dish"];
        DateTime newDate = Request.Form["start-date"];

        Restaurant newRestaurant = new Restaurant(newName, newFavDish, newDate, 1);
        newRestaurant.Save();

        List<Restaurant> allRestaurants = Restaurant.GetAll();
        return View["restaurants.cshtml", allRestaurants];
      };
    }
  }
}
