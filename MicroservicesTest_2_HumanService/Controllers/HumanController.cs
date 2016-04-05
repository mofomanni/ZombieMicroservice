using System;
using System.Linq;
using System.Web.Http;
using MicroservicesTest_2_HumanService.Models;

namespace MicroservicesTest_2_HumanService.Controllers
{
    public class HumanController : ApiController
    {
        [Route("api/human/bite")] // override the standard route pattern
        [HttpGet]
        public ZombieSighting HumanGetsBitten()
        {

            string correlationId = this.ControllerContext.Request.Headers.GetValues("BGT-CorrelationId").FirstOrDefault();

            Guid g = Guid.NewGuid();

            return new ZombieSighting
                {
                    Latitude = new Random().NextDouble(),
                    Longitude = new Random().NextDouble(),
                    Name = "Human-Zombie_" + g,
                    ZombieId = g
                };
        }
    }
}
