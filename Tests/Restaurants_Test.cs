using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Xunit;

namespace Yelp
{
  public class RestaurantTest : IDisposable
  {
    public RestaurantTest()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=yelp_test;Integrated Security=SSPI;";
    }
    [Fact]
    public void OverrideEquals_TwoSameRestaurants_Same()
    {//Arrange, Act
      DateTime testDate = new DateTime(2016,4,30);
      Restaurant firstRestaurant = new Restaurant("Wendys","nuggets",testDate,1);
      Restaurant secondRestaurant = new Restaurant("Wendys","nuggets",testDate,1);

      //Assert
      Assert.Equal(firstRestaurant,secondRestaurant);
    }

    public void Dispose()
    {
      Restaurant.DeleteAll();
    }
  }
}
