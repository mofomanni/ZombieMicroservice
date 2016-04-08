using System.Collections.Generic;
using System.Linq;
using MicroservicesTest2.Models;
using MicroservicesTest2.Utils.Logging;

namespace MicroservicesTest2.Persistency
{
    public class ZombiePersistency:IZombiePersistency
    {
        public List<ZombieSighting> GetAll()
        {
            using (var db = new ZombieContext())
            {
                return db.ZombieSightings.ToList();   
            }
        }

        public ZombieSighting GetById(string id)
        {
            using (var db = new ZombieContext())
            {
                return  db.ZombieSightings.FirstOrDefault(item => item.Name.Equals(id));
            }
        }

        public void Add(ZombieSighting zombie)
        {
            using (var db = new ZombieContext())
            {
                db.ZombieSightings.Add(zombie);
                db.SaveChanges();
            }
        }
    }
}