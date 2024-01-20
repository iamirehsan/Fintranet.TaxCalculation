using Fintranet.TaxCalculation.Model.Entities.Base;
using Fintranet.TaxCalculation.Repository.Implimentation;
using Fintranet.TaxCalculation.Repository.RepositoryInterfaces;

namespace Fintranet.TaxCalculation.Repository.Implimentation.RepositoryImplimentations
{
    public class CityRepository : Repository<City>, ICityRepository
    {
        public CityRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
