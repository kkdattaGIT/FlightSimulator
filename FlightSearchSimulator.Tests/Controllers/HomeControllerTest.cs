using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FlightSearchSimulator;
using FlightSearchSimulator.Controllers;
using Moq;
using FlightSearchSimulator.Repositories;
using FlightSearchSimulator.Interfaces;
using System.Collections.Specialized;
using FlightSearchSimulator.Models;
using System.Threading.Tasks;
using FlightSearchSimulator.Helpers;

namespace FlightSearchSimulator.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        //private string AirlineLogoAddress;

        [TestMethod]
        public void Index()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Search_ReturnsCollectionOfTypeSearchResult()
        {
            var flightSearchRepository = new Mock<IFlightSearchRepository>();
            var helper = new Mock<IHelper>();
            NameValueCollection Parameters = new NameValueCollection();
            Parameters.Add("DepartureAirportCode", "MEL");
            IEnumerable<SearchResult> SearchRes = Enumerable.Empty<SearchResult>();
            var MokResult = new Mock<IEnumerable<SearchResult>>();
            helper.Setup(e => e.GetBaseUrl()).Returns("http://localhost");

            flightSearchRepository.Setup(e => e.Search(
                new List<Provider> { new Provider() { ProviderUri  = "",
                                                      JsonDataPropertyName = ""
                                                    }
                                    }, Parameters)).Returns(Task.FromResult<IEnumerable<SearchResult>>(SearchRes)) ;

            HomeController sut = new HomeController(flightSearchRepository.Object, helper.Object);

            var actual = sut.Search();
            Assert.IsInstanceOfType(((ViewResult)actual.Result).Model,typeof(List<SearchResult>));
            

        }

    }
}
