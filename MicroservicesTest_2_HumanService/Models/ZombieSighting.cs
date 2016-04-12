using System;
using Newtonsoft.Json;

namespace MicroservicesTest_2_HumanService.Models
{
    public class ZombieSighting
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
}