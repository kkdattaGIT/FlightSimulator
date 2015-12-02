using FlightSearchSimulator.Interfaces;
using FlightSearchSimulator.Models;
using FlightSearchSimulator.Repositories;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace FlightSearchSimulator.Controllers
{
    public class SearchFlightsController : ApiController
    {
        private List<Provider> ProviderList = new List<Provider>();

        public async Task<IEnumerable<SearchResult>> Get()
        {
            createProviderData();
            NameValueCollection Parameters = HttpUtility.ParseQueryString(Request.RequestUri.Query);
            FlightSearchRepository FS = new FlightSearchRepository();
            var results = await FS.Search(ProviderList, Parameters);
           
            return results;
         }

 
        private void createProviderData()
        {
            Provider P = new Provider();
            P.ProviderUri = "http://nmflightapi.azurewebsites.net/api/AirlineOne";
            P.JsonDataPropertyName = "";
            Provider P2 = new Provider();
            P2.ProviderUri = "http://nmflightapi.azurewebsites.net/api/AirlineTwo";
            P2.JsonDataPropertyName = "Results";
            ProviderList.Add(P);
            ProviderList.Add(P2);           

        }
        }
}
