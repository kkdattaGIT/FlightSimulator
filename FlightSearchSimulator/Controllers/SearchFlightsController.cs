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
using System.Web.Http;

namespace FlightSearchSimulator.Controllers
{
    public class SearchFlightsController : ApiController
    {
        private List<Provider> ProviderList = new List<Provider>();

        public async Task<IEnumerable<SearchResult>> Get()
        {
            createProviderData();
            NameValueCollection Parameters = new NameValueCollection();
            Parameters.Add("DepartureAirportCode", "MEL");
            FlightSearchRepository FS = new FlightSearchRepository();
            var results = await FS.Search(ProviderList, Parameters);
           
            return results;
         }

        private SearchRequest createSearchRequest()
        {
            SearchRequest Srequest = new SearchRequest();
            Srequest.DeptAirportCode = "MEl";
            Srequest.ArrAirportCode = "LHR";
            Srequest.DeptDate = DateTime.Parse("2012-12-24T00:00:00+11:00");
            Srequest.ArrDate = DateTime.Parse("2013-01-03T00:00:00+11:00");
            return Srequest;
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
