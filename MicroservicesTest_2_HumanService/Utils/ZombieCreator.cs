using System;
using MicroservicesTest_2_HumanService.Models;

namespace MicroservicesTest_2_HumanService.Utils
{
    public class ZombieCreator : IZombieCreator
    {
        public ZombieSighting AwakeNewZombie()
        {
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