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

      public void Save()
      {
        SqlConnection conn = DB.Connection();
        conn.Open();

        SqlCommand cmd = new SqlCommand("INSERT INTO cuisines (name) OUTPUT INSERTED.id VALUES (@CuisineName);", conn);


        SqlParameter nameParameter = new SqlParameter();
        nameParameter.ParameterName = "@CuisineName";
        nameParameter.Value = this.GetName();
        cmd.Parameters.Add(nameParameter);
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

    public static Cuisine Find(int id)
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM cuisines WHERE id = @CuisineId;", conn);

      SqlParameter idParameter = new SqlParameter();
      idParameter.ParameterName = "@CuisineId";
      idParameter.Value = id.ToString();
      cmd.Parameters.Add(idParameter);
      SqlDataReader rdr = cmd.ExecuteReader();

      int foundId = 0;
      string foundName = null;

      while (rdr.Read())
      {
        foundId = rdr.GetInt32(0);
        foundName = rdr.GetString(1);
      }

      Cuisine foundCuisine = new Cuisine(foundName, foundId);

      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }

      return foundCuisine;
    }

    public List<Restaurant> GetRestaurant()
    {
      List<Restaurant> allRestaurants = new List<Restaurant>{};

      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM restaurants WHERE cuisine_id =@CuisineId ORDER BY cast([opening_date] as datetime) asc;", conn);

      SqlParameter idParameter = new SqlParameter();
      idParameter.ParameterName = "@CuisineId";
      idParameter.Value = this.GetId();
      cmd.Parameters.Add(idParameter);
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

    public static void DeleteAll()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("DELETE FROM cuisines;", conn);
      cmd.ExecuteNonQuery();
    }

    public void DeleteThisCuisine()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("DELETE FROM cuisines WHERE id = @CuisineId;", conn);
      SqlParameter idParameter = new SqlParameter();
      idParameter.ParameterName = "@CuisineId";
      idParameter.Value = this.GetId().ToString();
      cmd.Parameters.Add(idParameter);
      cmd.ExecuteNonQuery();
    }

    public void DeleteRestaurantInCuisine()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("DELETE FROM restaurants WHERE cuisine_id = @CuisineId;", conn);
      SqlParameter idParameter = new SqlParameter();
      idParameter.ParameterName = "@CuisineId";
      idParameter.Value = this.GetId().ToString();
      cmd.Parameters.Add(idParameter);
      cmd.ExecuteNonQuery();
    }

    public void Update(string newName)
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("UPDATE cuisines SET name = @NewName OUTPUT INSERTED.name WHERE id = @CuisineId;", conn);

      SqlParameter newNameParameter = new SqlParameter();
      newNameParameter.ParameterName = "@NewName";
      newNameParameter.Value = newName;
      cmd.Parameters.Add(newNameParameter);


      SqlParameter cuisineIdParameter = new SqlParameter();
      cuisineIdParameter.ParameterName = "@CuisineId";
      cuisineIdParameter.Value = this.GetId();
      cmd.Parameters.Add(cuisineIdParameter);
      SqlDataReader rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        //rdr.GetString(0) = INSERTED.name
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

  }
}
