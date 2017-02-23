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

    public static List<Restaurant> GetAll()
     {
       List<Restaurant> allRestaurants = new List<Restaurant>{};

       SqlConnection conn = DB.Connection();
       conn.Open();

       SqlCommand cmd = new SqlCommand("SELECT * FROM restaurants ORDER BY cast([opening_date] as datetime) asc;", conn);
       SqlDataReader rdr = cmd.ExecuteReader();

       while(rdr.Read())
       {
         int restaurantId = rdr.GetInt32(0);
         string restaurantName = rdr.GetString(1);
         string favDish = rdr.GetString(2);
         DateTime openingDate = rdr.GetDateTime(3);
         int cuisineId = rdr.GetInt32(4);
         Restaurant newRestaurant = new Restaurant(restaurantName, favDish, openingDate, cuisineId, restaurantId);
         allRestaurants.Add(newRestaurant);
       }

       if (rdr != null)
       {
         rdr.Close();
       }
       if (conn != null)
       {
         conn.Close();
       }

       return allRestaurants;
     }

    public void Save()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("INSERT INTO restaurants (name, fav_dish, opening_date, cuisine_id) OUTPUT INSERTED.id VALUES (@RestaurantName, @RestaurantFavDish, @RestaurantDate, @RestaurantCuisineId);", conn);

      SqlParameter nameParameter = new SqlParameter();
      nameParameter.ParameterName = "@RestaurantName";
      nameParameter.Value = this.GetName();

      SqlParameter favDishParameter = new SqlParameter();
      favDishParameter.ParameterName = "@RestaurantFavDish";
      favDishParameter.Value = this.GetFavDish();

      SqlParameter dateParameter = new SqlParameter();
      dateParameter.ParameterName = "@RestaurantDate";
      dateParameter.Value = this.GetDate();

      SqlParameter cuisineIdParameter = new SqlParameter();
      cuisineIdParameter.ParameterName = "@RestaurantCuisineId";
      cuisineIdParameter.Value = this.GetCuisineId();

      cmd.Parameters.Add(nameParameter);
      cmd.Parameters.Add(favDishParameter);
      cmd.Parameters.Add(dateParameter);
      cmd.Parameters.Add(cuisineIdParameter);

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

    public static Restaurant Find(int id)
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM restaurants WHERE id = @RestaurantId;", conn);

      SqlParameter idParameter = new SqlParameter();
      idParameter.ParameterName = "@RestaurantId";
      idParameter.Value = id.ToString();
      cmd.Parameters.Add(idParameter);
      SqlDataReader rdr = cmd.ExecuteReader();

      int foundId = 0;
      string foundName = null;
      string foundDish = null;
      DateTime foundDate = new DateTime();
      int foundCuisineId = 0;

      while (rdr.Read())
      {
        foundId = rdr.GetInt32(0);
        foundName = rdr.GetString(1);
        foundDish = rdr.GetString(2);
        foundDate = rdr.GetDateTime(3);
        foundCuisineId = rdr.GetInt32(4);
      }

      Restaurant foundRestaurant = new Restaurant(foundName,foundDish,foundDate,foundCuisineId, foundId);

      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }

      return foundRestaurant;
    }

    public void UpdateName(string newName)
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("UPDATE restaurants SET name = @NewName OUTPUT INSERTED.name WHERE id=@RestaurantId;", conn);

      SqlParameter newNameParameter = new SqlParameter();
      newNameParameter.ParameterName = "@NewName";
      newNameParameter.Value= newName;
      cmd.Parameters.Add(newNameParameter);

      SqlParameter restaurantIdParameter = new SqlParameter();
      restaurantIdParameter.ParameterName = "@RestaurantId";
      restaurantIdParameter.Value= this.GetId();
      cmd.Parameters.Add(restaurantIdParameter);
      SqlDataReader rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        this._name = rdr.GetString(0);
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

    public void UpdateDate(DateTime newDate)
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("UPDATE restaurants SET opening_date = @NewDate OUTPUT INSERTED.opening_date WHERE id=@RestaurantId;", conn);

      SqlParameter newDateParameter = new SqlParameter();
      newDateParameter.ParameterName = "@NewDate";
      newDateParameter.Value= newDate;
      cmd.Parameters.Add(newDateParameter);

      SqlParameter restaurantIdParameter = new SqlParameter();
      restaurantIdParameter.ParameterName = "@RestaurantId";
      restaurantIdParameter.Value= this.GetId();
      cmd.Parameters.Add(restaurantIdParameter);
      SqlDataReader rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        this._date = rdr.GetDateTime(0);
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

      SqlCommand cmd = new SqlCommand("DELETE FROM restaurants;", conn);
      cmd.ExecuteNonQuery();
    }

    public void DeleteThisRestaurant()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("DELETE FROM restaurants WHERE id = @RestaurantId;", conn);
      SqlParameter idParameter = new SqlParameter();
      idParameter.ParameterName = "@RestaurantId";
      idParameter.Value = this.GetId().ToString();
      cmd.Parameters.Add(idParameter);
      cmd.ExecuteNonQuery();
    }

  }
}
