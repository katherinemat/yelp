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

    public void Dispose()
    {
      Restaurant.DeleteAll();
    }
  }
}
