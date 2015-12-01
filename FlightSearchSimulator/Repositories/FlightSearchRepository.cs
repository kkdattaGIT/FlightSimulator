using FlightSearchSimulator.Interfaces;
using System;
using System.Collections.Generic;
using FlightSearchSimulator.Models;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Specialized;

namespace FlightSearchSimulator.Repositories
{
    public class FlightSearchRepository : IFlightSearchRepository
    {

        public async Task<IEnumerable<SearchResult>> Search(List<Provider> ProviderList, NameValueCollection parameters)
        {
            IEnumerable<SearchResult> results = null;
            Task<IEnumerable<SearchResult>>[] tasks = new Task<IEnumerable<SearchResult>>[ProviderList.Count()];
            int i = 0;
            foreach (var item in ProviderList)
            {
                tasks[i] = DoSearch(item.ProviderUri, item.JsonDataPropertyName);
                i++;
            }
            await Task.WhenAll(tasks);
            foreach (var item in tasks)
            {
                if (results == null)
                { results = item.Result; }
                else { results = results.Concat(item.Result); }
            }
            return results;
            //return DoSearch(url, parameters).Result.ToList();
        }

       private async Task<IEnumerable<SearchResult>> DoSearch(string uri, string PropertyName)
        {
           
            var ss = FlightSearchResources.FSContent.DeptAirportCodeDisplay;
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

    }
}