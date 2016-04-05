using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MicroservicesTest2.Models;

namespace MicroservicesTest2.Persistency
{
    public interface IZombiePersistency
    {
        List<ZombieSighting> GetAll();
        ZombieSighting GetById(string id);
        void Add(ZombieSighting zombie);
    }
}
