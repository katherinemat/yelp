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
        return View["cuisines.cshtml",allCuisines];
      };
    }
  }
}
