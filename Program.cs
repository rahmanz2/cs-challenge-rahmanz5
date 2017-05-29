using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.IO;
using Newtonsoft.Json;

namespace ElectricCarSearcher
{
  public class Program
  {
    public static string[] items = File.ReadAllLines("./metadata.txt");

    public static void Main(string[] args)
    {
      // Get all vehicles for 2017 
      string key = items[0];
      string year = items[1];
      string apiCallOnly = items[2];

      Console.WriteLine("Getting all vehicles for 2017...");
      string makesPath = string.Concat(new string[] { "./", year, "/edmunds-get-makes.txt" });
      string allMakes = "";

      try
      {
        allMakes = File.ReadAllText(makesPath);
      }
      catch (Exception ex)
      {
        allMakes = null;
      }

      string responseAsString = allMakes;

      if (string.IsNullOrEmpty(allMakes) || apiCallOnly ==  "true")
      {
        Console.WriteLine("No 2017 list of makes on disk OR apiCall was set to true in metadata... making API call");
        var client = new HttpClient();
        client.BaseAddress = new Uri(string.Format("https://api.edmunds.com/api/vehicle/v2/makes?state=new&year={0}&view=basic&fmt=json&api_key={1}", year, key));
        var response = client.GetAsync("").Result;

        responseAsString = response.Content.ReadAsStringAsync().Result;
        try
        {
          Directory.CreateDirectory(year);
          File.WriteAllText(makesPath, responseAsString);
        }
        catch (Exception ex)
        {
          Console.WriteLine("Error saving API data to file...will continue");
        }
      }
      else
      {
        Console.WriteLine("Found a disk version of {0} make data", year);
      }

      Makes makes = JsonConvert.DeserializeObject<Makes>(responseAsString);
      List<string> styleIds = new List<string>();

      // get all styles for all makes
      Console.WriteLine("Finding all unique styleIds across all makes and models...");
      foreach (var make in makes.makes)
      {
        foreach (var model in make.models)
        {
          if (styleIds.Contains(model.years[0].id))
          {
            Console.WriteLine("Duplicate style Id {0}, {1}, {2}, {3}, skip API call for detailed style", model.years[0].id, model.years[0].year, model.name, model.niceName);
          }
          else
          {
            styleIds.Add(model.years[0].id);
            InspectEngine(model.years[0].id, key, year, apiCallOnly);
          }
        }
      }
      Console.WriteLine("Check file output for more detail");
      Console.ReadLine();
    }

    private static void InspectEngine(string styleID, string key, string year, string apiCallOnly)
    {
      string localStyle = "";

      try
      {
        localStyle = File.ReadAllText("./edmunds-styleId-full-" + styleID);
      } catch (Exception ex) {
        localStyle = null;
      }
      
      string responseString = localStyle;

      string stylePath = string.Concat(new string[] { "./", year, "./edmunds-styleId-full-", styleID, ".txt" });

      if (string.IsNullOrEmpty(localStyle) || string.Compare(apiCallOnly, "true") == 0)
      {
        var client = new HttpClient();
        client.BaseAddress = new Uri(string.Format("https://api.edmunds.com/api/vehicle/v2/styles/{1}?view=full&fmt=json&api_key={0}", key, styleID));
        var response = client.GetAsync("").Result;

        responseString = response.Content.ReadAsStringAsync().Result;

        File.WriteAllText(stylePath, responseString);
      }

      if (responseString.Contains("\"fuelType\":\"electric\"") == true)
      {
        Console.WriteLine(responseString.Substring(0, 200));
      }
    }
  }
}
