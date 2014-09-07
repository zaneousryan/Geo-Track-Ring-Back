using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Hackathon2.Web;
using Hackathon2.Web.Controllers;

namespace Hackathon2.Web.Tests.Controllers
{
    [TestClass]
    public class ServicesControllerTest
    {
        [TestMethod]
        public void GetStopsForPassenger()
        {
            // Arrange
            ServicesController controller = new ServicesController();

            // Act
            var result = controller.GetStopsForPassenger(1);

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void CreateOuting()
        {
            // Arrange
            ServicesController controller = new ServicesController();

            // Act
            var result = controller.CreateOuting(3);

            // Assert
            Assert.IsNotNull(result);
        }

    }
}
