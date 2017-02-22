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
      Get["/cuisines/{id}"] = parameters => {
        Dictionary<string, object> model = new Dictionary<string, object>{};
        var foundCuisine = Cuisine.Find(parameters.id);
        var foundRestaurants = foundCuisine.GetRestaurant();
        model.Add("cuisine",foundCuisine);
        model.Add("restaurants",foundRestaurants);
        return View["cuisine.cshtml", model];
      };
      Get["/restaurants/{id}"] = parameters => {
        Dictionary<string, object> model = new Dictionary<string, object>{};
        var foundRestaurant = Restaurant.Find(parameters.id);
        var foundCuisine = Cuisine.Find(foundRestaurant.GetCuisineId());
        model.Add("cuisine",foundCuisine);
        model.Add("restaurant",foundRestaurant);
        return View["restaurant.cshtml", model];
      };
      Post["/cuisines/deleted"] = _ => {
        Cuisine.DeleteAll();
        List<Cuisine> allCuisines = Cuisine.GetAll();
        return View["cuisines.cshtml", allCuisines];
      };
      Post["/restaurants/deleted"] = _ => {
        Restaurant.DeleteAll();
        List<Restaurant> allRestaurants = Restaurant.GetAll();
        return View["restaurants.cshtml", allRestaurants];
      };
    }
  }
}
