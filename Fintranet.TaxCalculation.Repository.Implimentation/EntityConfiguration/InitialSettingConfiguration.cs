using Fintranet.TaxCalculation.Infrastructure.Enum;
using Fintranet.TaxCalculation.Infrastructure.Helpers;
using Fintranet.TaxCalculation.Model.Entities.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Newtonsoft.Json;

namespace Fintranet.TaxCalculation.Repository.Implimentation.EntityConfiguration
{
    public class InitialSettingConfiguration : BaseEntityConfiguration<InitialSetting>
    {
        public override void Configure(EntityTypeBuilder<InitialSetting> builder)
        {

            base.Configure(builder);
            builder.Property(x => x.IsDeleted).HasDefaultValue(false);

            builder.Property(p => p.FreeTaxDates).HasColumnName("freeTaxDates")
    .HasConversion(
                data => JsonConvert.SerializeObject(data),
            data => JsonConvert.DeserializeObject<IEnumerable<DateTime>>(data),
            new ValueComparer<IEnumerable<DateTime>>(
            (c1, c2) => c1.SequenceEqual(c2),
            c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
            c => c.AsEnumerable())
            );

            builder.Property(p => p.FreeTaxVehicleTypes).HasColumnName("freeTaxVehicleTypes")
             .HasConversion(
              data => data.ToJson(),
              data => ObjectUtils.ParseJson<IEnumerable<Guid>>(data),
              new ValueComparer<IEnumerable<Guid>>(
            (c1, c2) => c1.SequenceEqual(c2),
            c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
            c => c.AsEnumerable())
            );

            builder.Property(p => p.FreeTaxMonth).HasColumnName("freeTaxMonth")
             .HasConversion(
              data => data.ToJson(),
              data => ObjectUtils.ParseJson<IEnumerable<Month>>(data),
              new ValueComparer<IEnumerable<Month>>(
            (c1, c2) => c1.SequenceEqual(c2),
            c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
            c => c.AsEnumerable())
            );

            builder.Property(p => p.TaxPrices).HasColumnName("taxPrices")
                .HasConversion(
              data => data.ToJson(),
              data => ObjectUtils.ParseJson<IEnumerable<TaxPrice>>(data),
              new ValueComparer<IEnumerable<TaxPrice>>(
            (c1, c2) => c1.SequenceEqual(c2),
            c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
            c => c.AsEnumerable())
            );

        }
    }
}
