using FlightSearchResources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FlightSearchSimulator.Models
{
    public class SearchResult
    {
        [Display(Name = "AirlineLogoAddressLabel", ResourceType = typeof(FSContent))]
        public string AirlineLogoAddress { get; set; }

        [Display(Name = "AirlineNameLabel", ResourceType = typeof(FSContent))]
        public string AirlineName { get; set; }

        [Display(Name = "InboundFlightsDurationLabel", ResourceType = typeof(FSContent))]
        public string InboundFlightsDuration { get; set; }

        public string ItineraryId { get; set; }

        [Display(Name = "OutboundFlightsDurationLabel", ResourceType = typeof(FSContent))]
        public string OutboundFlightsDuration { get; set; }

        public int Stops { get; set; }

        [Display(Name = "TotalAmount", ResourceType = typeof(FSContent))]
        public decimal TotalAmount { get; set; }

    }

   
}