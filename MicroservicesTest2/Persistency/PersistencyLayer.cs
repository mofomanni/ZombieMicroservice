﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MicroservicesTest2.Models;

namespace MicroservicesTest2.Persistency
{
    public interface IPersistencyLayer
    {
        List<ZombieSighting> GetAllZombies();
        ZombieSighting GetZombieById(string id);
        void AddZombie(ZombieSighting zombie);
    }

    public class PersistencyLayer : IPersistencyLayer
    {
        private readonly IZombiePersistency _zombiePersistency;
        public PersistencyLayer(IZombiePersistency zombiePersistency)
        {
            _zombiePersistency = zombiePersistency;
        }

        public List<ZombieSighting> GetAllZombies()
        {
            return _zombiePersistency.GetAll();
        }

        public ZombieSighting GetZombieById(string id)
        {
            return _zombiePersistency.GetById(id);
        }

        public void AddZombie(ZombieSighting zombie)
        {
            _zombiePersistency.Add(zombie);
        }

    }
}