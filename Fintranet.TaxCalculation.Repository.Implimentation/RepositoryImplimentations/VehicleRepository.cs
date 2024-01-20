using Fintranet.TaxCalculation.Repository.Implimentation;
using Fintranet.TaxCalculation.Repository.RepositoryInterfaces;

namespace Fintranet.TaxCalculation.Repository.Implimentation.RepositoryImplimentations
{
    public class VehicleRepository : Repository<Vehicle>, IVehicleRepository
    {
        public VehicleRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
