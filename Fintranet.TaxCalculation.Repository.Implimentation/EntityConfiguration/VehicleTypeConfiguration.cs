using Fintranet.TaxCalculation.Model.Entities.Base;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Fintranet.TaxCalculation.Repository.Implimentation.EntityConfiguration
{
    public class VehicleTypeConfiguration : BaseEntityConfiguration<VehicleType>
    {
        public override void Configure(EntityTypeBuilder<VehicleType> builder)
        {
            base.Configure(builder);
    
     
        }
    }
}
