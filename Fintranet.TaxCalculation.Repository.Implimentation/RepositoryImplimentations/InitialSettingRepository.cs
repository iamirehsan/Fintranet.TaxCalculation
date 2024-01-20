using Fintranet.TaxCalculation.Model.Entities.Base;
using Fintranet.TaxCalculation.Repository.Implimentation;
using Fintranet.TaxCalculation.Repository.RepositoryInterfaces;

namespace Fintranet.TaxCalculation.Repository.Implimentation.RepositoryImplimentations
{
    public class InitialSettingRepository : Repository<InitialSetting>, IInitialSettingRepository
    {
        public InitialSettingRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
