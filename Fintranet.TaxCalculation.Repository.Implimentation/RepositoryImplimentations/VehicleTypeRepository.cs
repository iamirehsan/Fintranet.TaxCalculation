using Fintranet.TaxCalculation.Model.Entities.Base;
using Fintranet.TaxCalculation.Repository.Implimentation;
using Fintranet.TaxCalculation.Repository.RepositoryInterfaces;

namespace Fintranet.TaxCalculation.Repository.Implimentation.RepositoryImplimentations
{
    public class VehicleTypeRepository : Repository<VehicleType>, IVehicleTypeRepository
    {
        public VehicleTypeRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
