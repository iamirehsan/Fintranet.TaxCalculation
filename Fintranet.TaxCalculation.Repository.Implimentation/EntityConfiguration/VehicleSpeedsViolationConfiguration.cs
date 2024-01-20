using Fintranet.TaxCalculation.Model.Entities.Base;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fintranet.TaxCalculation.Repository.Implimentation.EntityConfiguration
{
    public class VehicleTaxDateConfiguration : BaseEntityConfiguration<VehicleTaxDate>
    {
        public override void Configure(EntityTypeBuilder<VehicleTaxDate> builder)
        {
            base.Configure(builder);
            builder.Ignore(z => z.TimeBaseOnHourAndMinuteAsString);


        }
    }
}
