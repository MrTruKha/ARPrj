using ARPrj.DataAccess;
using ARPrj.DataAccess.Repositories;
using ARPrj.Model.Models.AirLine;
using PAS.DataAccess.Common;
using PAS.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARPrj.Services.Service
{
    public interface IAirlineService
    {
        List<AirlineModel> GetAirlines();
        void CreateAirline(AirlineModel model);
    }
    class AirlineService :EntityService<Airline>, IAirlineService
    {
        private readonly IAirlineRepository _airlineRepository;
        public AirlineService(IAirlineRepository airlineRepository, IUnitOfWork unitOfWork) : base(unitOfWork, airlineRepository)
        {
            _airlineRepository = airlineRepository;
        }

        public void CreateAirline(AirlineModel model)
        {
            Insert(model.MapToEntity());
        }

        public List<AirlineModel> GetAirlines()
        {
            var result = _airlineRepository.GetAirlines().MapToModels();
            //or
            var result2 = GetAll().ToList().MapToModels();
            return result;
        }
    }
}
