using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Yelp
{
  public class Review
  {
    private int _id;
    private string _review;
    private int _rating;
    private int _restaurantId;

    public Review(string Review, int Rating, int RestaurantId, int Id = 0)
    {
      _id = Id;
      _review = Review;
      _rating = Rating;
      _restaurantId = RestaurantId;
    }

    public override bool Equals(System.Object otherReview)
    {
      if (!(otherReview is Review))
      {
        return false;
      }
      else
      {
        Review newReview = (Review) otherReview;
        bool idEquality = (this.GetId() == newReview.GetId());
        bool nameEquality = (this.GetReview() == newReview.GetReview());
        bool ratingEquality = (this.GetRating() == newReview.GetRating());
        bool restaurantIdEquality = (this.GetRestaurantId() == newReview.GetRestaurantId());
        return (idEquality && nameEquality && ratingEquality && restaurantIdEquality);
      }
    }

    public int GetId()
    {
      return _id;
    }

    public string GetReview()
    {
      return _review;
    }
    public void SetReview(string newReview)
    {
      _review = newReview;
    }

    public int GetRating()
    {
      return _rating;
    }
    public void SetRating(int newRating)
    {
      _rating = newRating;
    }

    public int GetRestaurantId()
    {
      return _restaurantId;
    }
    public void SetRestaurantId(int newRestaurantId)
    {
      _restaurantId = newRestaurantId;
    }

    public static List<Review> GetAll()
     {
       List<Review> allReviews = new List<Review>{};

       SqlConnection conn = DB.Connection();
       conn.Open();

       SqlCommand cmd = new SqlCommand("SELECT * FROM reviews;", conn);
       SqlDataReader rdr = cmd.ExecuteReader();

       while(rdr.Read())
       {
         int id = rdr.GetInt32(0);
         string review = rdr.GetString(1);
         int rating = rdr.GetInt32(2);
         int restaurantId = rdr.GetInt32(3);
         Review newReview = new Review(review, rating, restaurantId, id);
         allReviews.Add(newReview);
       }

       if (rdr != null)
       {
         rdr.Close();
       }
       if (conn != null)
       {
         conn.Close();
       }

       return allReviews;
     }

      public void Save()
      {
        SqlConnection conn = DB.Connection();
        conn.Open();

        SqlCommand cmd = new SqlCommand("INSERT INTO reviews (review, rating, restaurant_id) OUTPUT INSERTED.id VALUES (@Review, @Rating, @RestaurantId);", conn);

        SqlParameter reviewParameter = new SqlParameter();
        reviewParameter.ParameterName = "@Review";
        reviewParameter.Value = this.GetReview();

        SqlParameter ratingParameter = new SqlParameter();
        ratingParameter.ParameterName = "@Rating";
        ratingParameter.Value = this.GetRating();

        SqlParameter restaurantIdParameter = new SqlParameter();
        restaurantIdParameter.ParameterName = "@RestaurantId";
        restaurantIdParameter.Value = this.GetRestaurantId();

        cmd.Parameters.Add(reviewParameter);
        cmd.Parameters.Add(ratingParameter);
        cmd.Parameters.Add(restaurantIdParameter);
        SqlDataReader rdr = cmd.ExecuteReader();

        while(rdr.Read())
        {
          this._id = rdr.GetInt32(0);
        }

        if (rdr != null)
        {
          rdr.Close();
        }
        if (conn != null)
        {
          conn.Close();
        }
      }

    public static void DeleteAll()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("DELETE FROM reviews;", conn);
      cmd.ExecuteNonQuery();
    }

  }
}
