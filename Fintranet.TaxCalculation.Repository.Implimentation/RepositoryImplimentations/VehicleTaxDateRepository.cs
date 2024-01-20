using Fintranet.TaxCalculation.Model.Entities.Base;
using Fintranet.TaxCalculation.Repository.Implimentation;
using Fintranet.TaxCalculation.Repository.RepositoryInterfaces;

namespace Fintranet.TaxCalculation.Repository.Implimentation.RepositoryImplimentations
{
    public class VehicleTaxDateRepository : Repository<VehicleTaxDate>, IVehicleTaxDateRepository
    {
        public VehicleTaxDateRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
