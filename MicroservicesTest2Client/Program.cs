using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace MicroservicesTest2Client
{
    public partial class ZombieSighting
    {
        [JsonProperty(PropertyName = "ZombieId")]
        public Guid ZombieId { get; set; }

        [JsonProperty(PropertyName = "Name")]
        public String Name { get; set; }

        [JsonProperty(PropertyName = "Latitude")]
        public Double Latitude { get; set; }

        [JsonProperty(PropertyName = "Longitude")]
        public Double Longitude { get; set; }
    }

    class Program
    {
        static void Main()
        {
            RunAsync().Wait();


            Console.Read();
        }

        static async Task RunAsync()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:65313/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                // HTTP GET
                HttpResponseMessage response = await client.GetAsync("api/v7/zombies");
                if (response.IsSuccessStatusCode)
                {
                    List<ZombieSighting> zombieSightinga = await response.Content.ReadAsAsync<List<ZombieSighting>>();

                    foreach (var zombie in zombieSightinga)
                    {
                        Console.WriteLine("{0}\t{1}\t{2}\t{3}", zombie.Name, zombie.Latitude, zombie.Longitude, zombie.ZombieId);
                    }

                    Console.WriteLine("---------------------------------------------------");
                }


                // HTTP POST
                var gizmo = new ZombieSighting { Name = "Gizmo_" + Guid.NewGuid(), Latitude = 100.0, Longitude = 200.5, ZombieId = Guid.NewGuid() };
                response = await client.PostAsJsonAsync("api/v7/add", gizmo);
                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("response.IsSuccessStatusCode: " + response.IsSuccessStatusCode);
                    Console.WriteLine("---------------------------------------------------");
                }

                response = await client.GetAsync("api/v7/get/" + gizmo.Name);
                if (response.IsSuccessStatusCode)
                {
                    ZombieSighting zombieSighting = await response.Content.ReadAsAsync<ZombieSighting>();
                    Console.WriteLine("{0}\t{1}\t{2}\t{3}", zombieSighting.Name, zombieSighting.Latitude, zombieSighting.Longitude, zombieSighting.ZombieId);

                    Console.WriteLine("---------------------------------------------------");
                }


              

                response = await client.GetAsync("api/v7/bite");
                if (response.IsSuccessStatusCode)
                {
                    ZombieSighting zombieSighting = await response.Content.ReadAsAsync<ZombieSighting>();
                    Console.WriteLine("{0}\t{1}\t{2}\t{3}", zombieSighting.Name, zombieSighting.Latitude, zombieSighting.Longitude, zombieSighting.ZombieId);

                    Console.WriteLine("---------------------------------------------------");
                }

           }
        }
    }
}
