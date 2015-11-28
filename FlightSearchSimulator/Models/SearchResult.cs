using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FlightSearchSimulator.Models
{
    public class SearchResult
    {
        [Display(Name = "Airline logo")]
        public string AirlineLogoAddress { get; set; }

        [Display(Name = "Airline name")]
        public string AirlineName { get; set; }

        [Display(Name = "Inbound flight duration")]
        public string InboundFlightsDuration { get; set; }

        public string ItineraryId { get; set; }
        
        [Display(Name = "Outbound flight duration")]
        public string OutboundFlightsDuration { get; set; }

        public int Stops { get; set; }

        [Display(Name = "Total amount in USD")]
        public decimal TotalAmount { get; set; }

    }

   
}