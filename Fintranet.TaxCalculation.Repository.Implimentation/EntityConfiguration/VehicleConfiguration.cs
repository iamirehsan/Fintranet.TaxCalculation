using Fintranet.TaxCalculation.Model.Entities.Base;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fintranet.TaxCalculation.Repository.Implimentation.EntityConfiguration
{
    public class VehicleConfiguration : BaseEntityConfiguration<Vehicle>
    {
        public override void Configure(EntityTypeBuilder<Vehicle> builder)
        {
            base.Configure(builder);


        }
    }
}
