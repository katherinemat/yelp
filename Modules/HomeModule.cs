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
        var allCuisines = Cuisine.GetAll();
        var allRestaurants = Restaurant.GetAll();
        Dictionary<string, object> model = new Dictionary<string, object>{};
        model.Add("cuisineList", allCuisines);
        model.Add("restaurantList", allRestaurants);
        return View["restaurants.cshtml", model];
      };
      Post["/restaurants/new"] = _ =>{
        // Create new instance
        string newName = Request.Form["name"];
        string newFavDish = Request.Form["fav-dish"];
        DateTime newDate = Request.Form["start-date"];
        int newCuisineId = Request.Form["cuisine-id"];

        var newRestaurant = new Restaurant(newName, newFavDish, newDate, newCuisineId);
        newRestaurant.Save();

        //List out all instances
        var allCuisines = Cuisine.GetAll();
        var allRestaurants = Restaurant.GetAll();
        Dictionary<string, object> model = new Dictionary<string, object>{};
        model.Add("cuisineList", allCuisines);
        model.Add("restaurantList", allRestaurants);
        return View["restaurants.cshtml", model];
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
        var allCuisines = Cuisine.GetAll();
        var restaurantReviews = foundRestaurant.GetRestaurantReview();
        model.Add("cuisines", allCuisines);
        model.Add("cuisine", foundCuisine);
        model.Add("restaurant", foundRestaurant);
        model.Add("reviews", restaurantReviews);
        return View["restaurant.cshtml", model];
      };
      Delete["/cuisines/deleted"] = _ => {
        Cuisine.DeleteAll();
        List<Cuisine> allCuisines = Cuisine.GetAll();
        return View["cuisines.cshtml", allCuisines];
      };
      Delete["/restaurants/deleted"] = _ => {
        Restaurant.DeleteAll();
        var allCuisines = Cuisine.GetAll();
        var allRestaurants = Restaurant.GetAll();
        Dictionary<string, object> model = new Dictionary<string, object>{};
        model.Add("cuisineList", allCuisines);
        model.Add("restaurantList", allRestaurants);
        return View["restaurants.cshtml", model];
      };
      Delete["/cuisine/{id}/cleared"] = parameters => {
        Dictionary<string, object> model = new Dictionary<string, object>{};
        var foundCuisine = Cuisine.Find(parameters.id);
        foundCuisine.DeleteRestaurantInCuisine();
        var foundRestaurants = foundCuisine.GetRestaurant();
        model.Add("cuisine",foundCuisine);
        model.Add("restaurants",foundRestaurants);
        return View["cuisine.cshtml", model];
      };
      Delete["/cuisine/{id}/deleted"] = parameters => {
        Cuisine foundCuisine = Cuisine.Find(parameters.id);
        foundCuisine.DeleteThisCuisine();
        List<Cuisine> allCuisines = Cuisine.GetAll();
        return View["cuisines.cshtml", allCuisines];
      };
      Delete["/restaurant/{id}/deleted"] = parameters => {
        Restaurant foundRestaurant = Restaurant.Find(parameters.id);
        foundRestaurant.DeleteThisRestaurant();
        var allCuisines = Cuisine.GetAll();
        var allRestaurants = Restaurant.GetAll();
        Dictionary<string, object> model = new Dictionary<string, object>{};
        model.Add("cuisineList", allCuisines);
        model.Add("restaurantList", allRestaurants);
        return View["restaurants.cshtml", model];
      };
      Patch["cuisine/edit/{id}"] = parameters => {
        Cuisine foundCuisine = Cuisine.Find(parameters.id);
        foundCuisine.Update(Request.Form["cuisine-name"]);
        Dictionary<string, object> model = new Dictionary<string, object>{};
        var updatedCuisine = Cuisine.Find(parameters.id);
        var foundRestaurants = foundCuisine.GetRestaurant();
        model.Add("cuisine",updatedCuisine);
        model.Add("restaurants",foundRestaurants);
        return View["cuisine.cshtml", model];
      };
      Patch["restaurant/edit/{id}"] = parameters => {
        Restaurant foundRestaurant = Restaurant.Find(parameters.id);
        foundRestaurant.UpdateName(Request.Form["restaurant-name"]);
        foundRestaurant.UpdateDate(Request.Form["restaurant-date"]);
        foundRestaurant.UpdateCuisineId(Request.Form["restaurant-cuisineid"]);
        foundRestaurant.UpdateFavDish(Request.Form["restaurant-fav-dish"]);
        Dictionary<string, object> model = new Dictionary<string, object>{};
        var updatedRestaurant = Restaurant.Find(parameters.id);
        var foundCuisine = Cuisine.Find(foundRestaurant.GetCuisineId());
        var allCuisines = Cuisine.GetAll();
        var restaurantReviews = foundRestaurant.GetRestaurantReview();
        model.Add("cuisines", allCuisines);
        model.Add("cuisine", foundCuisine);
        model.Add("restaurant", updatedRestaurant);
        model.Add("reviews", restaurantReviews);
        return View["restaurant.cshtml", model];
      };
      Post["/cuisines/search"] = _ => {
        string searchTerm = Request.Form["search"];
        List<Cuisine> cuisineMatches = Cuisine.Search(searchTerm);
        return View["cuisine_search.cshtml", cuisineMatches];
      };
      Post["/restaurant/review/{id}"] = parameters => {
        Restaurant foundRestaurant = Restaurant.Find(parameters.id);
        string review = Request.Form["review"];
        int rating = Request.Form["rating"];
        int restaurantId = Request.Form["restaurantId"];
        Review newReview = new Review(review, rating, restaurantId);
        newReview.Save();

        Dictionary<string, object> model = new Dictionary<string, object>{};
        var restaurantReviews = foundRestaurant.GetRestaurantReview();
        var foundCuisine = Cuisine.Find(foundRestaurant.GetCuisineId());
        var allCuisines = Cuisine.GetAll();
        model.Add("reviews", restaurantReviews);
        model.Add("restaurant", foundRestaurant);
        model.Add("cuisine", foundCuisine);
        model.Add("cuisines", allCuisines);

        return View["restaurant.cshtml", model];
      };
    }
  }
}
