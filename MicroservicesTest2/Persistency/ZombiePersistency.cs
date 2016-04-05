using System.Collections.Generic;
using System.Linq;
using MicroservicesTest2.Models;
using MicroservicesTest2.Utils.Logging;

namespace MicroservicesTest2.Persistency
{
    public class ZombiePersistency:IZombiePersistency
    {
        public ZombiePersistency()
        {
            
        }

        public List<ZombieSighting> GetAll()
        {
            using (var db = new ZombieContext())
            {
                //var tempList = db.ZombieSightings.ToList();

                return db.ZombieSightings.ToList();

                //List<ZombieSighting> ret = new List<ZombieSighting>();

                //foreach (var z in tempList)
                //{
                //    var zl = new ZombieLoggingInfo
                //    {
                //        ZombieId = z.ZombieId,
                //        Latitude = z.Latitude,
                //        Longitude = z.Longitude,
                //        Name = z.Name
                //    };

                //    ret.Add(zl);
            //}
            //return ret;
                
            }
        }

        public ZombieSighting GetById(string id)
        {
            using (var db = new ZombieContext())
            {
                var z = db.ZombieSightings.FirstOrDefault(item => item.Name.Equals(id));

                if (z != null)
                {
                    var zl = new ZombieLoggingInfo
                    {
                        //ZombieId = z.ZombieId,
                        //Latitude = z.Latitude,
                        //Longitude = z.Longitude,
                        //Name = z.Name
                    };

                    return z;
                }

                return null;
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