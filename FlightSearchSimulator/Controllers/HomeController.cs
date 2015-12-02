using FlightSearchSimulator.Helpers;
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
        private readonly IHelper helper;

        public HomeController(IFlightSearchRepository flightSearchRepository, IHelper helper)
        {
            this.flightSearchRepository = flightSearchRepository;
            this.helper = helper;
        }

        public HomeController() : this(new FlightSearchRepository(),new Helper())
        {

        }
        public ActionResult Index()
        {
            SearchRequest sr = new SearchRequest();
            return View(sr);
        }

        public async Task<ActionResult> Search(SearchRequest req)
        {
            NameValueCollection Parameters = new NameValueCollection();
            List<Provider> ProviderList = new List<Provider>();
            Provider P = new Provider();
            Helper helper = new Helper();
            P.ProviderUri = this.helper.GetBaseUrl() + "/api/SearchFlights";
            P.JsonDataPropertyName = "";
            ProviderList.Add(P);
            foreach (var item in req.GetType().GetProperties().ToList())
            {
                Parameters.Add(item.Name,item.GetValue(req,null).ToString());
            }
            
            IEnumerable<SearchResult> results = await this.flightSearchRepository.Search(ProviderList, Parameters);

            return View(results);
        }


    }
}