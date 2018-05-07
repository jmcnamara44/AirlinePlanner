using System.Collections.Generic;
using MySql.Data.MySqlClient;
using AirlinePlannerApp;
using System;
using Microsoft.AspNetCore.Mvc;

namespace AirlinePlannerApp.Models
{
  public class Cities
  {
    private int _id;
    private string _name;

    public Cities(string name, int id = 0)
    {
      _id = id;
      _name = name;
    }

    public int GetId()
    {
      return _id;
    }
    public string GetName()
    {
      return _name;
    }
    public void SetName(string Name)
    {
      _name = Name;
    }

    public static List<Cities> GetAll()
      {
          List<Cities> allCities = new List<Cities> {};
          MySqlConnection conn = DB.Connection();
          conn.Open();
          MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
          cmd.CommandText = @"SELECT * FROM cities;";
          MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
          while(rdr.Read())
          {
            int id = rdr.GetInt32(0);
            string name = rdr.GetString(1);
            Cities newCities = new Cities(name, id);
            allCities.Add(newCities);
          }
          conn.Close();
          if (conn != null)
          {
              conn.Dispose();
          }
          return allCities;
      }
      // public static void DeleteAll()
      // {
      //     MySqlConnection conn = DB.Connection();
      //     conn.Open();
      //     var cmd = conn.CreateCommand() as MySqlCommand;
      //     cmd.CommandText = @"DELETE FROM cities;";
      //     cmd.ExecuteNonQuery();
      //     conn.Close();
      //     if (conn != null)
      //     {
      //         conn.Dispose();
      //     }
      // }
  }
}
