using Fintranet.TaxCalculation.Infrastructure.Enum;
using Fintranet.TaxCalculation.Model.Entities.Base;

namespace Fintranet.TaxCalculation.Test.Test.Domain.Test
{
    public class InitialSettingTest
    {
        [Fact]
        public void Create_WithValidParameters_InstanceIsCreatedCorrectly()
        {
            // Arrange
            bool taxFreeOnHolidays = true;
            double? maximumTaxPerDay = 50.0;
            IEnumerable<DateTime>? freeTaxDates = new List<DateTime> { DateTime.Now.Date };
            IEnumerable<Month>? freeTaxMonth = new List<Month> { Month.January };
            IEnumerable<Guid>? freeTaxVehicleTypes = new List<Guid> { Guid.NewGuid() };
            IEnumerable<TaxPrice>? taxPrices = new List<TaxPrice> { TaxPrice.Create("09:00", "17:00", 10.0) };
            Guid cityId = Guid.NewGuid();

            // Act
            var initialSetting = InitialSetting.Create(
                taxFreeOnHolidays, freeTaxDates, freeTaxMonth, freeTaxVehicleTypes, taxPrices, cityId, maximumTaxPerDay
            );

            // Assert
            Assert.NotNull(initialSetting);
            Assert.Equal(taxFreeOnHolidays, initialSetting.TaxFreeOnHolidays);
            Assert.Equal(maximumTaxPerDay, initialSetting.MaximumTaxPerDay);
            Assert.Equal(freeTaxDates, initialSetting.FreeTaxDates);
            Assert.Equal(freeTaxMonth, initialSetting.FreeTaxMonth);
            Assert.Equal(freeTaxVehicleTypes, initialSetting.FreeTaxVehicleTypes);
            Assert.Equal(taxPrices, initialSetting.TaxPrices);
            Assert.Equal(cityId, initialSetting.CityId);
        }

        [Fact]
        public void Create_WithNullParameters_InstanceIsCreatedCorrectly()
        {
            // Arrange
            bool taxFreeOnHolidays = false;
            double? maximumTaxPerDay = null;
            IEnumerable<DateTime>? freeTaxDates = null;
            IEnumerable<Month>? freeTaxMonth = null;
            IEnumerable<Guid>? freeTaxVehicleTypes = null;
            IEnumerable<TaxPrice>? taxPrices = null;
            Guid cityId = Guid.NewGuid();

            // Act
            var initialSetting = InitialSetting.Create(
                taxFreeOnHolidays, freeTaxDates, freeTaxMonth, freeTaxVehicleTypes, taxPrices, cityId
            );

            // Assert
            Assert.NotNull(initialSetting);
            Assert.Equal(taxFreeOnHolidays, initialSetting.TaxFreeOnHolidays);
            Assert.Equal(maximumTaxPerDay, initialSetting.MaximumTaxPerDay);
            Assert.Equal(freeTaxDates, initialSetting.FreeTaxDates);
            Assert.Equal(freeTaxMonth, initialSetting.FreeTaxMonth);
            Assert.Equal(freeTaxVehicleTypes, initialSetting.FreeTaxVehicleTypes);
            Assert.Equal(taxPrices, initialSetting.TaxPrices);
            Assert.Equal(cityId, initialSetting.CityId);
        }
    }
}
