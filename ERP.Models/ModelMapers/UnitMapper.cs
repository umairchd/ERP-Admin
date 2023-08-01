using ERP.Models.DomainModels;
using ERP.Models.WebModels;

namespace ERP.Models.ModelMapers
{
    public static class UnitMapper
    {
        public static Unit CreateFromClientToServer(this UnitModel unitModel)
        {
            return new Unit()
            {
               UnitId = unitModel.UnitId,
               UnitTitle = unitModel.UnitTitle,
               UnitValue = unitModel.UnitValue,
               RecCreatedBy = unitModel.RecCreatedBy,
               RecCreatedDate = unitModel.RecCreatedDate,
               RecLastUpdatedBy = unitModel.RecLastUpdatedBy,
               RecLastUpdatedDate = unitModel.RecLastUpdatedDate,
            };
        }
        public static UnitModel CreateFromServerToClient(this Unit unit)
        {
            return new UnitModel()
            {
                UnitId = unit.UnitId,
                UnitTitle = unit.UnitTitle,
                UnitValue = unit.UnitValue,
                RecCreatedBy = unit.RecCreatedBy,
                RecCreatedDate = unit.RecCreatedDate,
                RecLastUpdatedBy = unit.RecLastUpdatedBy,
                RecLastUpdatedDate = unit.RecLastUpdatedDate,
            };
        }
    }
}