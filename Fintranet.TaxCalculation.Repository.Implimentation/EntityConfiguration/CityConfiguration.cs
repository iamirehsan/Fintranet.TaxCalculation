using Fintranet.TaxCalculation.Model.Entities.Base;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fintranet.TaxCalculation.Repository.Implimentation.EntityConfiguration
{
    public class CityConfiguration : BaseEntityConfiguration<City>
    {
        public override void Configure(EntityTypeBuilder<City> builder)
        {
            base.Configure(builder);


        }
    }
}
