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

    public void Dispose()
    {
      Cuisine.ClearAll();
      Restaurant.DeleteAll();
    }
  }
}
