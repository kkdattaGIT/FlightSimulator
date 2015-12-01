using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FlightSearchSimulator;
using FlightSearchSimulator.Controllers;

namespace FlightSearchSimulator.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
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
        //public void Search_ReturnsCollectionOfTypeSearchResult()
        //{
        //    var flightSearchRepository = new Mock<IFlightSearchRepository>();
        //    flightSearchRepository.Setup(...)

        //    HomeController sut = new HomeController(flightSearchRepository.Object);

        //    var actual = sut.Search();

        //}

    }
}
