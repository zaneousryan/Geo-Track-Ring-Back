using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Hackathon2.Web.Services;
using System.Threading.Tasks;
using Hackathon2.Entity;

namespace Hackathon2.Web.Tests
{
    [TestClass]
    public class PuppyServiceTest
    {
        [TestMethod]
        public async Task GetPossibleStopsForPassenger()
        {
            await new PuppyServices().GetPossibleStopsForPassenger(3, null);
        }

        [TestMethod]
        public async Task CreateOuting()
        {
            await new PuppyServices().CreateOuting(3);
        }


        [TestMethod]
        public async Task AddStop()
        {
            await new PuppyServices().AddStop(1, "Circus Circus", Convert.ToDecimal(36.111000), Convert.ToDecimal(-115.173100));
        }


        [TestMethod]
        public async Task RockThePuppy()
        {
            await new PuppyServices().RockThePuppy(3, false);
        }

        [TestMethod]
        public async Task RockThePuppyComplete()
        {
            await new PuppyServices().RockThePuppy(3, true);
        }


        [TestMethod]
        public async Task RockThePuppyLogic()
        {
            await new Logic().RockThePuppy(1, false);
        }

        [TestMethod]
        public async Task RockThePuppyCompleteLogic()
        {
            await new Logic().RockThePuppy(1, true);
        }
    }
}
