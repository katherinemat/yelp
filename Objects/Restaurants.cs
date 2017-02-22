using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Yelp
{
  public class Restaurant
  {
    private int _id;
    private string _name;
    private string _favDish;
    private DateTime _date;
    private int _cuisineId;

    public Restaurant(string Name, string FavDish, DateTime Date, int CuisineId, int Id = 0)
    {
      _id = Id;
      _name = Name;
      _favDish = FavDish;
      _date = Date;
      _cuisineId = CuisineId;
    }

    public override bool Equals(System.Object otherRestaurant)
    {
      if (!(otherRestaurant is Restaurant))
      {
        return false;
      }
      else
      {
        Restaurant newRestaurant = (Restaurant) otherRestaurant;
        bool idEquality = (this.GetId() == newRestaurant.GetId());
        bool nameEquality = (this.GetName() == newRestaurant.GetName());
        bool favDishEquality = (this.GetFavDish() == newRestaurant.GetFavDish());
        bool dateEquality = (this.GetDate() == newRestaurant.GetDate());
        bool cuisineIdEquality = (this.GetCuisineId() == newRestaurant.GetCuisineId());
        return (idEquality && nameEquality && favDishEquality && dateEquality && cuisineIdEquality);
      }
    }

    public int GetId()
    {
      return _id;
    }

    public string GetName()
    {
      return _name;
    }
    public void SetName(string newName)
    {
      _name = newName;
    }

    public string GetFavDish()
    {
      return _favDish;
    }
    public void SetFavDish(string newFavDish)
    {
      _favDish = newFavDish;
    }

    public DateTime GetDate()
    {
      return _date;
    }
    public void SetDate(DateTime newDate)
    {
      _date = newDate;
    }

    public int GetCuisineId()
    {
      return _cuisineId;
    }
    public void SetCuisineId(int newCuisineId)
    {
      _cuisineId = newCuisineId;
    }

    // public static List<Restaurant> GetAll()
    //  {
    //    List<Restaurant> allRestaurants = new List<Restaurant>{};
    //
    //    SqlConnection conn = DB.Connection();
    //    conn.Open();
    //
    //    SqlCommand cmd = new SqlCommand("SELECT * FROM tasks ORDER BY cast([date] as datetime) asc;", conn);
    //    SqlDataReader rdr = cmd.ExecuteReader();
    //
    //    while(rdr.Read())
    //    {
    //      int taskId = rdr.GetInt32(0);
    //      string taskName = rdr.GetString(1);
    //      int FavDish = rdr.GetInt32(2);
    //      DateTime taskDate = rdr.GetDateTime(3);
    //      Restaurant newRestaurant = new Restaurant(taskName, FavDish, taskDate, taskId);
    //      allRestaurants.Add(newRestaurant);
    //    }
    //
    //    if (rdr != null)
    //    {
    //      rdr.Close();
    //    }
    //    if (conn != null)
    //    {
    //      conn.Close();
    //    }
    //
    //    return allRestaurants;
    //  }

    public static void DeleteAll()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("DELETE FROM restaurants;", conn);
      cmd.ExecuteNonQuery();
    }

  }
}
