using FlightSearchSimulator.Interfaces;
using FlightSearchSimulator.Models;
using FlightSearchSimulator.Repositories;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace FlightSearchSimulator.Controllers
{
    public class HomeController : Controller
    {
        private readonly IFlightSearchRepository flightSearchRepository;

        public HomeController(IFlightSearchRepository flightSearchRepository)
        {
            this.flightSearchRepository = flightSearchRepository;
        }

        public HomeController() : this(new FlightSearchRepository())
        {

        }
        public ActionResult Index()
        {
            SearchRequest sr = new SearchRequest();
            return View(sr);
        }

        //public ActionResult Search()
        //{
        //    NameValueCollection Parameters = new NameValueCollection();
        //    List<Provider> ProviderList = new List<Provider>();
        //    Provider P = new Provider();
        //    P.ProviderUri = Request.Url.GetLeftPart(UriPartial.Authority) + Url.Content("~") + "/api/SearchFlights";
        //    P.JsonDataPropertyName = "";
        //    ProviderList.Add(P);
        //    Parameters.Add("DepartureAirportCode", "MEL");
        //    FlightSearchRepository FS = new FlightSearchRepository();
        //    var results = FS.Search(ProviderList, Parameters);

        //    return View(results.Result);
        //}

        public async Task<ActionResult> Search()
        {
            IEnumerable<SearchResult> results = null;
            using (HttpClient client = new HttpClient())
            using (HttpResponseMessage response = await client.GetAsync(Request.Url.GetLeftPart(UriPartial.Authority) + Url.Content("~") + "/api/SearchFlights"))
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