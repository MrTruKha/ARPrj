using PAS.DataAccess.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARPrj.DataAccess.Repositories
{
    public interface IAirlineRepository : IBaseRepository<Airline>
    {
        List<Airline> GetAirlines();
    }
    class AirlineRepository:BaseRepository<Airline>,IAirlineRepository
    {
        public AirlineRepository(ARPrjEntities context) :base(context) 
        {

        }     
        public List<Airline> GetAirlines()
        {
            var result = Dbset.AsEnumerable().ToList();
            return result;
        }
    }
}
