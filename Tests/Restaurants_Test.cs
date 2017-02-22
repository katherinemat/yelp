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

    [Fact]
    public void GetAll_DatabaseEmptyAtFirst_NoRestaurants()
    {
      //Arrange, Act
      int output = Restaurant.GetAll().Count;

      //Assert
      Assert.Equal(0, output);
    }

    [Fact]
    public void Test_Save_SavesToDatabase()
    {
      //Arrange
      DateTime testDate = new DateTime(1999,6,4);
      Restaurant testRestaurant = new Restaurant("Wendys","nuggets",testDate,1);
      testRestaurant.Save();

      //Act
      List<Restaurant> result = Restaurant.GetAll();
      List<Restaurant> testList = new List<Restaurant>{testRestaurant};

      //Assert
      Assert.Equal(testList, result);
    }

    public void Dispose()
    {
      Restaurant.DeleteAll();
    }
  }
}
