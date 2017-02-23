using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Xunit;

namespace Yelp
{
  public class CuisineTest : IDisposable
  {
    public CuisineTest()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=yelp_test;Integrated Security=SSPI;";
    }

    [Fact]
    public void OverrideEquals_TwoSameCuisines_Same()
    {
      //Arrange, Act
      Cuisine firstCuisine = new Cuisine("western");
      Cuisine secondCuisine = new Cuisine("western");

      //Assert
      Assert.Equal(firstCuisine, secondCuisine);
    }

    [Fact]
    public void GetAll_DatabaseEmptyAtFirst_NoCuisines()
    {
      //Arrange,Act
      int result = Cuisine.GetAll().Count;

      //Assert
      Assert.Equal(0, result);
    }

    [Fact]
    public void Save_OneInstanceofCusine_SavesToDatabase()
    {
      //Arrange
      Cuisine testCuisine = new Cuisine("western");
      testCuisine.Save();

      //Act
      List<Cuisine> output = Cuisine.GetAll();
      List<Cuisine> verify = new List<Cuisine>{testCuisine};

      //Assert
      Assert.Equal(output,verify);
    }

    [Fact]
    public void SaveGetAll_OneInstanceofCuisine_AssignIdToInstance()
    {
      //Arrange
      Cuisine testCuisine = new Cuisine("western");
      testCuisine.Save();
      Cuisine savedCuisine = Cuisine.GetAll()[0];

      //Act
      int output = savedCuisine.GetId();
      int verify = testCuisine.GetId();

      //Assert
      Assert.Equal(output,verify);
    }

    [Fact]
    public void Find_CuisineClass_FoundCuisine()
    {
      //Arrange
      Cuisine testCuisine = new Cuisine("western");
      testCuisine.Save();

      //Act
      Cuisine foundCuisine = Cuisine.Find(testCuisine.GetId());

      //Assert
      Assert.Equal(testCuisine, foundCuisine);
    }

    [Fact]
    public void Search_CuisineName_CuisineMatches()
    {
      //Arrange
      Cuisine firstCuisine = new Cuisine("western");
      firstCuisine.Save();
      Cuisine secondCuisine = new Cuisine("japonese");
      secondCuisine.Save();

      //Act
      List<Cuisine> cuisineMatches = Cuisine.Search("western");

      List<Cuisine> expectedCuisines = new List<Cuisine>{firstCuisine};

      //Assert
      Assert.Equal(cuisineMatches, expectedCuisines);
    }

    [Fact]
    public void Search_CuisineName_CuisineDoesntMatch()
    {
      //Arrange
      Cuisine firstCuisine = new Cuisine("western");
      firstCuisine.Save();
      Cuisine secondCuisine = new Cuisine("japonese");
      secondCuisine.Save();

      //Act
      List<Cuisine> cuisineMatches = Cuisine.Search("westernrere");

      List<Cuisine> expectedCuisines = new List<Cuisine>{};

      //Assert
      Assert.Equal(cuisineMatches, expectedCuisines);
    }

    [Fact]
    public void GetRestaurant_TwoRestaurantsSameClass_RetrievedRestaurants()
    {
      //Arrange
      Cuisine testCuisine = new Cuisine("western");
      testCuisine.Save();

      DateTime testDate = new DateTime(2016,4,30);
      Restaurant firstRestaurant = new Restaurant("Wendys","nuggets",testDate,testCuisine.GetId());
      firstRestaurant.Save();

      Restaurant secondRestaurant = new Restaurant("McDonalds","Big Mac",testDate,testCuisine.GetId());
      secondRestaurant.Save();

      //Act
      List<Restaurant> output = testCuisine.GetRestaurant();
      List<Restaurant> verify = new List<Restaurant>{firstRestaurant,secondRestaurant};

      //Assert
      Assert.Equal(output,verify);
    }

    [Fact]
    public void DeleteThisCuisine_OneCuisine_CuisineDeleted()
    {//Arrange
      Cuisine firstCuisine = new Cuisine("japonese");
      firstCuisine.Save();
      Cuisine secondCuisine = new Cuisine("chinese");
      secondCuisine.Save();
      firstCuisine.DeleteThisCuisine();
      List<Cuisine> outputList = Cuisine.GetAll();

      //Act
      List<Cuisine> verifyList = new List<Cuisine>{secondCuisine};

      //Assert
      Assert.Equal(outputList,verifyList);
    }

    [Fact]
    public void DeleteRestaurantInCuisine_OneCuisine_CuisineEmpty()
    {//Arrange
      Cuisine testCuisine = new Cuisine("Western");
      testCuisine.Save();

      DateTime testDate = new DateTime(2016,4,30);
      Restaurant firstRestaurant = new Restaurant("Wendys","nuggets",testDate,testCuisine.GetId());
      firstRestaurant.Save();

      Restaurant secondRestaurant = new Restaurant("McDonalds","Big Mac",testDate,testCuisine.GetId());
      secondRestaurant.Save();

      testCuisine.DeleteRestaurantInCuisine();
      List<Restaurant> outputList = testCuisine.GetRestaurant();

      //Act
      List<Restaurant> verifyList = new List<Restaurant>{};

      //Assert
      Assert.Equal(outputList, verifyList);
    }

    [Fact]
    public void Update_OneCuisine_NewCuisineName()
    {
      string original = "Wendys";
      Cuisine newCuisine = new Cuisine(original);
      newCuisine.Save();
      string newString = "Burger King";
      newCuisine.Update(newString);
      string newCuisineName = newCuisine.GetName();

      Assert.Equal(newString, newCuisineName);
    }

    public void Dispose()
    {
      Cuisine.DeleteAll();
      Restaurant.DeleteAll();
    }
  }
}
