using System.Collections.Generic;
using MySql.Data.MySqlClient;
using AirlinePlannerApp;
using System;
using Microsoft.AspNetCore.Mvc;

namespace AirlinePlannerApp.Models
{
  public class Flights
  {
    private int _id;
    private string _departureTime;

    public Flights(string departureTime, int id = 0)
    {
      _id = id;
      _departureTime = departureTime;
    }

    public static List<Flights> GetAll()
    {
        List<Flights> allFlights = new List<Flights> {};
        MySqlConnection conn = DB.Connection();
        conn.Open();
        MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
        cmd.CommandText = @"SELECT * FROM flights;";
        MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
        while(rdr.Read())
        {
          int id = rdr.GetInt32(0);
          string departureTime = rdr.GetDateTime(1).ToString();
          Flights newFlights = new Flights(departureTime, id);
          allFlights.Add(newFlights);
        }
        conn.Close();
        if (conn != null)
        {
            conn.Dispose();
        }
        return allFlights;
    }

    public List<Cities> GetDepartureCities()
    {
        MySqlConnection conn = DB.Connection();
        conn.Open();
        MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
        cmd.CommandText = @"SELECT cities.* FROM flights
            JOIN cities_flights ON (flights.id = cities_flights.flight_id)
            JOIN cities ON (cities_flights.departure_city_id = cities.id)
            WHERE flights.id = @FlightId;";

        MySqlParameter flightIdParameter = new MySqlParameter();
        flightIdParameter.ParameterName = "@FlightId";
        flightIdParameter.Value = _id;
        cmd.Parameters.Add(flightIdParameter);

        MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
        List<Cities> cities = new List<Cities>{};

        while(rdr.Read())
        {
          int cityId = rdr.GetInt32(0);
          string cityName = rdr.GetString(1);
          Cities newCity = new Cities(cityName, cityId);
          cities.Add(newCity);
        }
        conn.Close();
        if (conn != null)
        {
            conn.Dispose();
        }
        return cities;
      }

    }
  }
