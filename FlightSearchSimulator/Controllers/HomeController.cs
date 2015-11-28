using FlightSearchSimulator.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace FlightSearchSimulator.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            SearchRequest sr = new SearchRequest();
            return View(sr);
        }

        public async Task<ActionResult> Search()
        {
            ViewBag.Message = "Your application description page.";
            IEnumerable<SearchResult> results = null;
            using (HttpClient client = new HttpClient())
            using (HttpResponseMessage response = await client.GetAsync(Request.Url.GetLeftPart(UriPartial.Authority) + Url.Content("~")+"/api/SearchFlights"))
            using (HttpContent content = response.Content)
            {
                var res = await content.ReadAsStringAsync();
                JObject json = JObject.Parse("{ \"Results\": " + res + "}");
                results = JsonConvert.DeserializeObject<IEnumerable<SearchResult>>(json.GetValue("Results").ToString());
            }

            return View(results);
        }

    }
}