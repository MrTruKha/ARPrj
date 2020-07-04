using ARPrj.DataAccess;
using ARPrj.Model.Models.AirLine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARPrj.Services
{
    public static class Mapper
    {
        #region Mapping Airline
        public static AirlineModel MapToModel(this Airline entity)
        {
            var airline = new AirlineModel
            {
                AirlineId = entity.AirlineId,
                AirlineName = entity.AirlineName,
                NetPrice = entity.NetPrice,
                CreateDate = entity.CreateDate,
                UpdateDate = entity.UpdateDate
            };
            return airline;
        }
        public static List<AirlineModel> MapToModels(this List<Airline> entities)
        {
            return entities.Select(x => x.MapToModel()).ToList();
        }
        public static Airline MapToEntity(this AirlineModel model)
        {
            var airline = new Airline
            {
                AirlineId = model.AirlineId,
                AirlineName = model.AirlineName,
                NetPrice = model.NetPrice,
                CreateDate = model.CreateDate,
                UpdateDate = model.UpdateDate
            };
            return airline;
        }
        #endregion
        #region MyRegion

        #endregion
    }
}
