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
    
    public void Dispose()
    {
      Restaurant.DeleteAll();
    }
  }
}
