using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using Castle.Core.Internal;
using MicroservicesTest2.Models;
using MicroservicesTest2.Persistency;
using MicroservicesTest2.Utils.Logging;

namespace MicroservicesTest2.Controllers
{
    public class ZombieController : ApiController
    {
        private readonly PersistencyLayer _persistencyLayer;

        public ZombieController(PersistencyLayer persistencyLayer)
        {
            _persistencyLayer = persistencyLayer;   
        }

        [Route("api/v7/zombies")] // override the standard route pattern
        [HttpGet]
        public IEnumerable<ZombieSighting> GetAllSightings()
        {
            List<ZombieSighting> sightings = _persistencyLayer.GetAllZombies();
            return sightings;
        }

        [Route("api/v7/get/{name}")] // override the standard route pattern
        [HttpGet]
        public ZombieSighting Get(string name)
        {
            return _persistencyLayer.GetZombieById(name);
        }

        [Route("api/v7/add")] // override the standard route pattern
        [HttpPost]
        public HttpResponseMessage Add([FromBody] ZombieSighting zombie)
        {
            _persistencyLayer.AddZombie(zombie);
            return new HttpResponseMessage(HttpStatusCode.Accepted);
        }


        [Route("api/v7/bite")] // override the standard route pattern
        [HttpGet]
        public async Task<ZombieSighting> BiteHuman()
        {
            
            ZombieSighting ret = await RunAsync();
            _persistencyLayer.AddZombie(ret);
            return ret;
        }

       
        async Task<ZombieSighting> RunAsync()
        {
            ZombieSighting zombieSighting = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:56085/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("BGT-CorrelationId", (string)this.ControllerContext.Request.Properties["BGT-CorrelationId"]);
                // HTTP GET
                HttpResponseMessage response = await client.GetAsync("api/human/bite");
                if (response.IsSuccessStatusCode)
                {
                    zombieSighting = await response.Content.ReadAsAsync<ZombieSighting>();
                 
                    Console.WriteLine("{0}\t${1}\t{2}", zombieSighting.Name, zombieSighting.Latitude, zombieSighting.Longitude);
                }
            }

            return zombieSighting;
        }
    }
}