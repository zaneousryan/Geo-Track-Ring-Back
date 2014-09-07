using Hackathon2.Entity;
using Hackathon2.Infrastructure;
using Hackathon2.TCS.ToolKit;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hackathon2.Web.Tests
{
    [TestClass]
    public class TCSToolKit
    {
        [TestMethod]
        public async Task TestGetDirections()
        {
            //33.559768,-117.728416&destination=33.574088,-117.7234
            Mock<ILocationInfo> origin = new Mock<ILocationInfo>();
            Mock<ILocationInfo> destination = new Mock<ILocationInfo>();

            origin.Setup(x => x.Latitude).Returns(Convert.ToDecimal(33.559768));
            origin.Setup(x => x.Longitude).Returns(Convert.ToDecimal(-117.728416));
            destination.Setup(x => x.Latitude).Returns(Convert.ToDecimal(33.574088));
            destination.Setup(x => x.Longitude).Returns(Convert.ToDecimal(-117.7234));

            LocationService service = new LocationService();

            var directions = await service.GetDirections(origin.Object, destination.Object);

        }

        [TestMethod]
        public async Task UpdateOutingState()
        {

            await new Logic().UpdateOutingState(3);


        }
        [TestMethod]
        public async Task UpdateActiveOutingPassengers()
        {
            await new Logic().UpdateActiveOutingPassengers();
        }

        [TestMethod]
        public async Task UpdateOutingText()
        {
            //33.559768,-117.728416&destination=33.574088,-117.7234
            Mock<ILocationInfo> origin = new Mock<ILocationInfo>();
            Mock<ILocationInfo> destination = new Mock<ILocationInfo>();

            origin.Setup(x => x.Latitude).Returns(Convert.ToDecimal(33.559768));
            origin.Setup(x => x.Longitude).Returns(Convert.ToDecimal(-117.728416));
            destination.Setup(x => x.Latitude).Returns(Convert.ToDecimal(33.574088));
            destination.Setup(x => x.Longitude).Returns(Convert.ToDecimal(-117.7234));

            var stop = new Stop()
            {
                Description = "My house",
                Latitude = Convert.ToDecimal(33.559768),
                Longitude = Convert.ToDecimal(-117.728416)
            };
            LocationService service = new LocationService();

            var directions = await service.GetRoute(origin.Object, destination.Object);
            var result = new Logic().UpdateOutingText(
                origin.Object,
                stop, directions);


        }


        [TestMethod]
        public async Task GetMap()
        {
            //33.559768,-117.728416&destination=33.574088,-117.7234
            Mock<ILocationInfo> origin = new Mock<ILocationInfo>();

            origin.Setup(x => x.Latitude).Returns(Convert.ToDecimal(33.559768));
            origin.Setup(x => x.Longitude).Returns(Convert.ToDecimal(-117.728416));

            LocationService service = new LocationService();

            var directions = await service.GetMap(origin.Object);

        }


        [TestMethod]
        public async Task TestGetRoute()
        {
            //33.559768,-117.728416&destination=33.574088,-117.7234
            Mock<ILocationInfo> origin = new Mock<ILocationInfo>();
            Mock<ILocationInfo> destination = new Mock<ILocationInfo>();

            origin.Setup(x => x.Latitude).Returns(Convert.ToDecimal(40.7464275));
            origin.Setup(x => x.Longitude).Returns(Convert.ToDecimal(-74.0144389));
            destination.Setup(x => x.Latitude).Returns(Convert.ToDecimal(40.75));
            destination.Setup(x => x.Longitude).Returns(Convert.ToDecimal(-73.866667));

            LocationService service = new LocationService();

            var directions = await service.GetRoute(origin.Object, destination.Object);

            var gauge = directions.CurrentGauge;

        }
    }
}
