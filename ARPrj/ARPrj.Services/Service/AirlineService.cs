using ARPrj.DataAccess;
using ARPrj.DataAccess.Common;
using ARPrj.DataAccess.Repositories;
using ARPrj.Model.Models.AirLine;
using ARPrj.Service;
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
        void DeleteAirline(AirlineModel model);
        void EditAirline(AirlineModel model);
    }
    public class AirlineService :EntityService<Airline>, IAirlineService
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

        public void DeleteAirline(AirlineModel model)
        {
            Delete(model.AirlineId);
            UnitOfWork.SaveChanges();
        }

        public void EditAirline(AirlineModel model)
        {
            var airLineEntity = _airlineRepository.FindAll(x => x.AirlineId == model.AirlineId).FirstOrDefault();
            if (airLineEntity != null)
            {
                airLineEntity.AirlineName = model.AirlineName;
                airLineEntity.CreateDate = model.CreateDate;
                airLineEntity.UpdateDate = model.UpdateDate;
                airLineEntity.NetPrice = model.NetPrice;
            }
            Update(airLineEntity);          
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
