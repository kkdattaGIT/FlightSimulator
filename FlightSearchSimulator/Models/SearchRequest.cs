using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Resources;
using System.ComponentModel;
using FlightSearchResources;

namespace FlightSearchSimulator.Models
{
    
    public class SearchRequest

    {
        [Required(ErrorMessageResourceName = "DeptAirportCodeReqError", ErrorMessageResourceType = typeof(FSContent))]
        [RegularExpression(@"^[a-zA-Z0-9 ]{3,3}$", ErrorMessageResourceName = "DeptAirportCodeReqLengthValidationMsg", ErrorMessageResourceType = typeof(FSContent))]
        [Display(Name = "DeptAirportCodeDisplay", ResourceType = typeof(FSContent))]
        public string DeptAirportCode { get; set; }

        [Required(ErrorMessageResourceName = "ArrAirportCodeReqError", ErrorMessageResourceType = typeof(FSContent))]
        [RegularExpression(@"^[a-zA-Z0-9 ]{3,3}$", ErrorMessageResourceName = "ArrAirportCodeReqLengthValidationMsg", ErrorMessageResourceType = typeof(FSContent))]
        [Display(Name = "ArrAirportCodeDisplay", ResourceType = typeof(FSContent))]
        public string ArrAirportCode { get; set; }

        [Required(ErrorMessageResourceName = "DeptDateReq", ErrorMessageResourceType = typeof(FSContent))]
        [Display(Name = "DeptDateDisplay", ResourceType = typeof(FSContent))]
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

        [Required(ErrorMessageResourceName = "ArrDateReq", ErrorMessageResourceType = typeof(FSContent))]
        [Display(Name = "ArrDateDisplay", ResourceType = typeof(FSContent))]
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