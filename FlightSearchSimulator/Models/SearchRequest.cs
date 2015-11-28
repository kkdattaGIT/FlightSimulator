using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FlightSearchSimulator.Models
{
    public class SearchRequest
    {
        [Required(ErrorMessage = "Departure airport code required.")]
        [RegularExpression(@"^[a-zA-Z0-9 ]{3,3}$", ErrorMessage = "Departure code must be alphanumeric and only 3 characters in length")]
        [Display(Name = "Departure airport code:")]
        public string DeptAirportCode { get; set; }

        [Required(ErrorMessage = "Arrival airport code required.")]
        [RegularExpression(@"^[a-zA-Z0-9 ]{3,3}$", ErrorMessage = "Arrival code must be alphanumeric and only 3 characters in length")]
        [Display(Name = "Arrival airport code:")]
        public string ArrAirportCode { get; set; }

        [Required(ErrorMessage = "Departure date required.")]
        [Display(Name = "Departure Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime DeptDate {
            get
            {
                return this._deptDate.HasValue
                   ? this._deptDate.Value
                   : DateTime.Now;
            }

            set { this._deptDate = value; }
        }

        [Required(ErrorMessage = "Arrival date required.")]
        [Display(Name = "Arrival Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime ArrDate {
            get
            {
                return this._arrDate.HasValue
                   ? this._arrDate.Value
                   : DateTime.Now;
            }

            set { this._arrDate = value; }
        }

        private DateTime? _arrDate = null;
        private DateTime? _deptDate = null;
    }
}