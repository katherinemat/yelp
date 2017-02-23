using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Xunit;

namespace Yelp
{
  public class ReviewTest : IDisposable
  {
    public ReviewTest()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=yelp_test;Integrated Security=SSPI;";
    }
    [Fact]
    public void OverrideEquals_TwoSameReviews_Same()
    {//Arrange, Act
      Review firstReview = new Review("great place", 5, 1);
      Review secondReview = new Review("great place", 5, 1);

      //Assert
      Assert.Equal(firstReview,secondReview);
    }

    [Fact]
    public void GetAll_DatabaseEmptyAtFirst_NoReviews()
    {
      //Arrange, Act
      int output = Review.GetAll().Count;

      //Assert
      Assert.Equal(0, output);
    }

    [Fact]
    public void Save_OneInstanceofReview_SavesToDatabase()
    {
      //Arrange
      Review testReview = new Review("great place", 5, 1);
      testReview.Save();

      //Act
      List<Review> result = Review.GetAll();
      List<Review> testList = new List<Review>{testReview};

      //Assert
      Assert.Equal(testList, result);
    }

    [Fact]
    public void SaveGetAll_OneInstanceofReviews_AssignIdToInstance()
    {
      //Arrange
      Review testReview = new Review("great place", 5, 1);

      //Act
      testReview.Save();
      Review savedReview = Review.GetAll()[0];

      int result = savedReview.GetId();
      int testId = testReview.GetId();

      //Assert
      Assert.Equal(testId, result);
    }

    [Fact]
    public void GetRestaurantReview_OneRestaurant_ReviewsAboutRestaurants()
    {
      //Act
      DateTime testDate = new DateTime(1999,6,4);
      Restaurant testRestaurant = new Restaurant("Wendys","nuggets",testDate,1);
      testRestaurant.Save();

      Review testReview1 = new Review("great place", 5, testRestaurant.GetId());
      testReview1.Save();
      Review testReview2 = new Review("so so place", 3, testRestaurant.GetId());
      testReview2.Save();

      //Arrange
      List<Review> output = testRestaurant.GetRestaurantReview();
      List<Review> verify = new List<Review>{testReview1,testReview2};

      //Act
      Assert.Equal(output,verify);
    }

    public void Dispose()
    {
      Review.DeleteAll();
    }
  }
}
