using FlightSearchSimulator.Models;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Threading.Tasks;

namespace FlightSearchSimulator.Interfaces
{
    public interface IFlightSearchRepository
    {
        Task<IEnumerable<SearchResult>> Search(List<Provider> ProviderList, NameValueCollection parameters);
    }
}