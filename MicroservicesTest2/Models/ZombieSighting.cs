using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace MicroservicesTest2.Models
{
    public class ZombieSighting
    {
        [JsonProperty(PropertyName = "BGT-CorrelationId")]
        [NotMapped]
        public string CorrelationId { get; set; }

        [JsonProperty(PropertyName = "ZombieId")]
        public Guid ZombieId { get; set; }

        [JsonProperty(PropertyName = "Name")]
        [Key]
        public String Name { get; set; }

        [JsonProperty(PropertyName = "Latitude")]
        public Double Latitude { get; set; }

        [JsonProperty(PropertyName = "Longitude")]
        public Double Longitude { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}