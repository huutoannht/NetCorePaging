using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using DataGridDemo.Model;
using System.Text;
using System.Reflection;
using System.Linq.Dynamic.Core;
using Newtonsoft.Json;

namespace DataGridDemo.Controllers
{
  public class HomeController : BaseController
  {
    private IHostingEnvironment hostingEnvironment;

    public HomeController(IHostingEnvironment env)
    {
      hostingEnvironment = env;
    }

    

    private IList<Country> countries
    {
      get
      {
        String fullFileName = System.IO.Path.Combine(hostingEnvironment.ContentRootPath, "Data", "countries.json");

        String doc = System.IO.File.ReadAllText(fullFileName);

        IList<Country> result = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Country>>(doc);

        return result;
      }
    }


    [HttpGet]
    public virtual ActionResult Load(String sort, String order, String search, Int32 limit, Int32 offset, String ExtraParam)
    {
      // Get entity fieldnames
      List<String> columnNames = typeof(Country).GetProperties(BindingFlags.Public | BindingFlags.Instance).Select(p => p.Name).ToList();

      // Create a seperate list for searchable field names   
      List<String> searchFields = new List<String>(columnNames);

      // Exclude field Iso2 for filtering 
      searchFields.Remove("ISO2");

      // Perform filtering
      IQueryable items = SearchItems(countries.AsQueryable(), search, searchFields);

      // Sort the filtered items and apply paging
      return Content(ItemsToJson(items, columnNames, sort, order, limit, offset), "application/json");
    }
  }
}
