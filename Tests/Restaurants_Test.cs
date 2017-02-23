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
    public void Save_OneInstanceofRestaurants_SavesToDatabase()
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

    [Fact]
    public void SaveGetAll_OneInstanceofRestaurants_AssignIdToInstance()
    {
      //Arrange
      DateTime testDate = new DateTime(1999,6,4);
      Restaurant testRestaurant = new Restaurant("Wendys","nuggets",testDate,1);

      //Act
      testRestaurant.Save();
      Restaurant savedRestaurant = Restaurant.GetAll()[0];

      int result = savedRestaurant.GetId();
      int testId = testRestaurant.GetId();

      //Assert
      Assert.Equal(testId, result);
    }

    [Fact]
    public void Find_RestaurantInDatabase_ReturnCorrectIdRestaurant()
    {
      //Arrange
      DateTime testDate = new DateTime(1999,6,4);
      Restaurant testRestaurant = new Restaurant("Wendys","nuggets",testDate,1);
      testRestaurant.Save();

      //Act
      Restaurant foundRestaurant = Restaurant.Find(testRestaurant.GetId());

      //Assert
      Assert.Equal (testRestaurant,foundRestaurant);
    }

    [Fact]
    public void DeleteThisRestaurant_OneRestaurant_RestaurantDeleted()
    {//Arrange
      DateTime testDate = new DateTime(2016,4,30);
      Restaurant firstRestaurant = new Restaurant("Wendys","nuggets",testDate,1);
      firstRestaurant.Save();
      Restaurant secondRestaurant = new Restaurant("McDonald","Big Mac",testDate,2);
      secondRestaurant.Save();
      firstRestaurant.DeleteThisRestaurant();
      List<Restaurant> outputList = Restaurant.GetAll();

      //Act
      List<Restaurant> verifyList = new List<Restaurant>{secondRestaurant};

      //Assert
      Assert.Equal(outputList,verifyList);
    }

    [Fact]
    public void UpdateName_OneRestaurant_NewName()
    {
      //Arrange
      DateTime testDate = new DateTime(2016,4,30);
      string originalName = "Wendys";
      Restaurant testRestaurant = new Restaurant(originalName,"nuggets", testDate, 1);
      testRestaurant.Save();
      string newName = "PapaJohns";
      testRestaurant.UpdateName(newName);
      //Act
      string outputName = testRestaurant.GetName();
      //Assert
      Assert.Equal(newName,outputName);
    }

    [Fact]
    public void UpdateDate_OneRestaurant_NewDate()
    {
      //Arrange
      DateTime testDate = new DateTime(2016,4,30);
      Restaurant testRestaurant = new Restaurant("Wendys","nuggets", testDate, 1);
      testRestaurant.Save();
      DateTime newDate = new DateTime(2014,7,30);
      testRestaurant.UpdateDate(newDate);
      //Act
      DateTime outputDate = testRestaurant.GetDate();
      //Assert
      Assert.Equal(newDate,outputDate);
    }

    [Fact]
    public void UpdateCuisineId_OneRestaurant_NewCuisineId()
    {
      //Arrange
      DateTime testDate = new DateTime(2016,4,30);
      Restaurant testRestaurant = new Restaurant("Wendys","nuggets", testDate, 1);
      testRestaurant.Save();
      int newCuisineId = 2;
      testRestaurant.UpdateCuisineId(newCuisineId);
      //Act
      int outputCuisineId = testRestaurant.GetCuisineId();
      //Assert
      Assert.Equal(newCuisineId, outputCuisineId);
    }

    public void Dispose()
    {
      Restaurant.DeleteAll();
    }
  }
}
