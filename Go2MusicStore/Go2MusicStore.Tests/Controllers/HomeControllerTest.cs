using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Go2MusicStore;
using Go2MusicStore.Controllers;

namespace Go2MusicStore.Tests.Controllers
{
    using System.Web.Caching;

    using Go2MusicStore.API.Interfaces;
    using Go2MusicStore.Controllers.Mvc;

    using NSubstitute;

    [TestClass]
    public class HomeControllerTest
    {
        private IApplicationManager applicationManager;

        [TestInitialize]
        public void Setup()
        {
            this.applicationManager = Substitute.For<IApplicationManager>();
        }

        [TestMethod]
        public void Index()
        {
            // Arrange
            HomeController controller = new HomeController(this.applicationManager);

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void About()
        {
            // Arrange
            HomeController controller = new HomeController(this.applicationManager);

            // Act
            ViewResult result = controller.About() as ViewResult;

            // Assert
            Assert.AreEqual("Your application description page.", result.ViewBag.Message);
        }

        [TestMethod]
        public void Contact()
        {
            // Arrange
            HomeController controller = new HomeController(this.applicationManager);

            // Act
            ViewResult result = controller.Contact() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }
    }
}
