using System.Collections.Generic;
using ERP.Interfaces.IRepository;
using ERP.Interfaces.IServices;
using ERP.Models.DomainModels;

namespace ERP.Service.Services
{
    public class UnitService:IUnitService
    {
        private readonly IUnitRepository unitRepository;

        public UnitService(IUnitRepository unitRepository)
        {
            this.unitRepository = unitRepository;
        }

        public IEnumerable<Unit> GetAllUnits()
        {
            return unitRepository.GetAll();
        }

        public Unit GetUnit(long id)
        {
            return unitRepository.Find(id);
        }

        public long AddUnit(Unit unit)
        {
            if (unit.UnitId > 0)
                unitRepository.Update(unit);
            else
                unitRepository.Add(unit);

            unitRepository.SaveChanges();
            return unit.UnitId;
        }
    }
}
