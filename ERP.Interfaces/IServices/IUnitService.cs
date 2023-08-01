using System.Collections.Generic;
using ERP.Models.DomainModels;

namespace ERP.Interfaces.IServices
{
    public interface IUnitService
    {
        IEnumerable<Unit> GetAllUnits();
        Unit GetUnit(long id);

        long AddUnit(Unit unit);

    }
}
