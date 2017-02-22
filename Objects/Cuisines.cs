using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Yelp
{
  public class Cuisine
  {
    private int _id;
    private string _name;

    public Cuisine(string Name, int Id = 0)
    {
      _id = Id;
      _name = Name;
    }

    public override bool Equals(System.Object otherCuisine)
    {
      if (!(otherCuisine is Cuisine))
      {
        return false;
      }
      else
      {
        Cuisine newCuisine = (Cuisine) otherCuisine;
        bool idEquality = (this.GetId() == newCuisine.GetId());
        bool nameEquality = (this.GetName() == newCuisine.GetName());
        return (idEquality && nameEquality);
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

    public static List<Cuisine> GetAll()
     {
       List<Cuisine> allCuisines = new List<Cuisine>{};

       SqlConnection conn = DB.Connection();
       conn.Open();

       SqlCommand cmd = new SqlCommand("SELECT * FROM cuisines;", conn);
       SqlDataReader rdr = cmd.ExecuteReader();

       while(rdr.Read())
       {
         int cuisineId = rdr.GetInt32(0);
         string cuisineName = rdr.GetString(1);
         Cuisine newCuisine = new Cuisine(cuisineName, cuisineId);
         allCuisines.Add(newCuisine);
       }

       if (rdr != null)
       {
         rdr.Close();
       }
       if (conn != null)
       {
         conn.Close();
       }

       return allCuisines;
     }
    //
    // public void Save()
    // {
    //   SqlConnection conn = DB.Connection();
    //   conn.Open();
    //
    //   SqlCommand cmd = new SqlCommand("INSERT INTO restaurants (name, fav_dish, opening_date, cuisine_id) OUTPUT INSERTED.id VALUES (@RestaurantName, @RestaurantFavDish, @RestaurantDate, @RestaurantCuisineId);", conn);
    //
    //   SqlParameter nameParameter = new SqlParameter();
    //   nameParameter.ParameterName = "@RestaurantName";
    //   nameParameter.Value = this.GetName();
    //
    //   SqlParameter favDishParameter = new SqlParameter();
    //   favDishParameter.ParameterName = "@RestaurantFavDish";
    //   favDishParameter.Value = this.GetFavDish();
    //
    //   SqlParameter dateParameter = new SqlParameter();
    //   dateParameter.ParameterName = "@RestaurantDate";
    //   dateParameter.Value = this.GetDate();
    //
    //   SqlParameter cuisineIdParameter = new SqlParameter();
    //   cuisineIdParameter.ParameterName = "@RestaurantCuisineId";
    //   cuisineIdParameter.Value = this.GetCuisineId();
    //
    //   cmd.Parameters.Add(nameParameter);
    //   cmd.Parameters.Add(favDishParameter);
    //   cmd.Parameters.Add(dateParameter);
    //   cmd.Parameters.Add(cuisineIdParameter);
    //
    //   SqlDataReader rdr = cmd.ExecuteReader();
    //
    //   while(rdr.Read())
    //   {
    //     this._id = rdr.GetInt32(0);
    //   }
    //
    //   if (rdr != null)
    //   {
    //     rdr.Close();
    //   }
    //   if (conn != null)
    //   {
    //     conn.Close();
    //   }
    // }
    //
    // public static Restaurant Find(int id)
    // {
    //   SqlConnection conn = DB.Connection();
    //   conn.Open();
    //
    //   SqlCommand cmd = new SqlCommand("SELECT * FROM restaurants WHERE id = @RestaurantId;", conn);
    //
    //   SqlParameter idParameter = new SqlParameter();
    //   idParameter.ParameterName = "@RestaurantId";
    //   idParameter.Value = id.ToString();
    //   cmd.Parameters.Add(idParameter);
    //   SqlDataReader rdr = cmd.ExecuteReader();
    //
    //   int foundId = 0;
    //   string foundName = null;
    //   string foundDish = null;
    //   DateTime foundDate = new DateTime();
    //   int foundCuisineId = 0;
    //
    //   while (rdr.Read())
    //   {
    //     foundId = rdr.GetInt32(0);
    //     foundName = rdr.GetString(1);
    //     foundDish = rdr.GetString(2);
    //     foundDate = rdr.GetDateTime(3);
    //     foundCuisineId = rdr.GetInt32(4);
    //   }
    //
    //   Restaurant foundRestaurant = new Restaurant(foundName,foundDish,foundDate,foundCuisineId, foundId);
    //
    //   if (rdr != null)
    //   {
    //     rdr.Close();
    //   }
    //   if (conn != null)
    //   {
    //     conn.Close();
    //   }
    //
    //   return foundRestaurant;
    // }

    public static void DeleteAll()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("DELETE FROM cuisines;", conn);
      cmd.ExecuteNonQuery();
    }

  }
}
