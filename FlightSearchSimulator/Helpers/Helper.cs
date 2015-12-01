using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FlightSearchSimulator.Helpers
{
    public class Helper : IHelper
    {
        public string GetBaseUrl()
        {
            return HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) ;
        }
    }
}