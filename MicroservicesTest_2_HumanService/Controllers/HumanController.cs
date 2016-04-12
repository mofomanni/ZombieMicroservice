using System.Web.Http;
using MicroservicesTest_2_HumanService.Models;
using MicroservicesTest_2_HumanService.Utils;

namespace MicroservicesTest_2_HumanService.Controllers
{
    public class HumanController : ApiController
    {
        private readonly IZombieCreator _zombieCreator;

        public HumanController(IZombieCreator zombieCreator)
        {
            _zombieCreator = zombieCreator;
        }

        [Route("api/human/bite")] // override the standard route pattern
        [HttpGet]
        public ZombieSighting HumanGetsBitten()
        {
            return _zombieCreator.AwakeNewZombie();
        }
    }
}
