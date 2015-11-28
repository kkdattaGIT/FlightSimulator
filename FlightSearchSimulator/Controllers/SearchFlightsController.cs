using FlightSearchSimulator.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
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
            IEnumerable<SearchResult> results = null;
            Task<IEnumerable<SearchResult>>[] tasks = new Task<IEnumerable<SearchResult>>[ProviderList.Count()];
            int i = 0;
            foreach (var item in ProviderList)
            {
                tasks[i] = GetFlightDetailsAsync(item.ProviderUri, item.JsonDataPropertyName);
                i++;                
            }
           await Task.WhenAll(tasks);
           foreach (var item in tasks)
            {if (results == null)
                {results=item.Result;}
                else {results = results.Concat(item.Result);}
            }
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

        private async Task<IEnumerable<SearchResult>> GetFlightDetailsAsync(string uri, string PropertyName)
            {
                IEnumerable<SearchResult> results = null;
                using (HttpClient client = new HttpClient())
                using (HttpResponseMessage response = await client.GetAsync(uri))
                using (HttpContent content = response.Content)
                    {
                        var res = await content.ReadAsStringAsync();
                        JObject json;
                        if (string.IsNullOrEmpty(PropertyName))
                            json = JObject.Parse("{ \"Results\": " + res + "}");
                        else
                            json = JObject.Parse(res);
                        results = JsonConvert.DeserializeObject<IEnumerable<SearchResult>>(json.GetValue("Results").ToString());
                    }
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
