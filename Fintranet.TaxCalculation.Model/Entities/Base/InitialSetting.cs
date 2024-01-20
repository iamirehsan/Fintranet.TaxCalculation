using Fintranet.TaxCalculation.Infrastructure.Enum;

namespace Fintranet.TaxCalculation.Model.Entities.Base
{
    public class InitialSetting : BaseEntity
    {
        public bool TaxFreeOnHolidays { get; private set; }
        public double? MaximumTaxPerDay { get; private set; }
        public IEnumerable<DateTime>? FreeTaxDates { get; private set; }
        public IEnumerable<Month>? FreeTaxMonth { get; private set; }
        public IEnumerable<Guid>? FreeTaxVehicleTypes { get; private set; }
        public IEnumerable<TaxPrice>? TaxPrices { get; private set; }
        public virtual City City { get; private set; }
        public Guid CityId { get; private set; }

        private InitialSetting(bool taxFreeOnHolidays, double? maximumTaxPerDay, IEnumerable<DateTime>? freeTaxDates, IEnumerable<Month>? freeTaxMonth,
            IEnumerable<Guid>? freeTaxVehicleTypes, IEnumerable<TaxPrice>? taxPrices, Guid cityId) : base()
        {
            TaxFreeOnHolidays = taxFreeOnHolidays;
            MaximumTaxPerDay = maximumTaxPerDay;
            FreeTaxDates = freeTaxDates;
            FreeTaxMonth = freeTaxMonth;
            FreeTaxVehicleTypes = freeTaxVehicleTypes;
            TaxPrices = taxPrices;
            CityId = cityId;
        }
        public static InitialSetting Create(bool taxFreeOnHolidays, IEnumerable<DateTime>? freeTaxDates, IEnumerable<Month>? freeTaxMonth,
            IEnumerable<Guid>? freeTaxVehicleTypes, IEnumerable<TaxPrice>? taxPrices, Guid cityId , double? maximumTaxPerDay = null)
        { return new InitialSetting(taxFreeOnHolidays, maximumTaxPerDay, freeTaxDates, freeTaxMonth, freeTaxVehicleTypes, taxPrices, cityId); }
    }
    public class TaxPrice
    {
        public string? StartingTime { get; private set; }
        public string? EndingTime { get; private set; }
        public double Price { get; private set; }

        private TaxPrice(string? startingTime, string? endingTime, double price)
        {
            StartingTime = startingTime;
            EndingTime = endingTime;
            Price = price;

        }
        public static TaxPrice Create(string? startingTime, string? endingTime, double price) { return new TaxPrice(startingTime, endingTime, price); }
    }
}
