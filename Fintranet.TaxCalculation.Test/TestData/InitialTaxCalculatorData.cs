using Fintranet.TaxCalculation.Infrastructure.Enum;
using Fintranet.TaxCalculation.Model.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fintranet.TaxCalculation.Test.TestData
{
    public static class InitialTaxCalculatorData
    {
        public static Guid CityId => Guid.Parse("aad1c289-2128-47cf-9bf9-4db5b8c33bdf");
        public static Guid VehicleId => Guid.Parse("aad1c289-2128-47cf-9bf9-4db5b8c33bdf");
        public static Guid FreeTaxVehicle => Guid.Parse("aad1c289-2128-47cf-9bf9-4db5b8c33bdf");
        public static Vehicle Vehicle => Vehicle.Create("bmw-i8", FreeTaxVehicle);
        public static IEnumerable<VehicleTaxDate> VehicleTaxDates => new List<VehicleTaxDate>(2) { VehicleTaxDate.Create(VehicleId, DateTime.Parse("2024-02-16 15:00:00")), VehicleTaxDate.Create(VehicleId, DateTime.Parse("2024-01-16 15:00:00")) };
        public static InitialSetting FreeTaxInitialSetting => InitialSetting.Create(true, Enumerable.Empty<DateTime>(), Enumerable.Empty<Month>(), new List<Guid>() { FreeTaxVehicle },
            Enumerable.Empty<TaxPrice>(), CityId);

        public static InitialSetting InitialSettingWithoutMaxNumber => InitialSetting.Create(true, Enumerable.Empty<DateTime>(), Enumerable.Empty<Month>(), Enumerable.Empty<Guid>()
            , new List<TaxPrice>() { TaxPrice.Create("14:00", "15:00", 20) }, CityId);


        public static InitialSetting InitialSettingWithoutMaxNumberAndFreeTaxMonth => InitialSetting.Create(true, Enumerable.Empty<DateTime>(), new List<Month>() { Month.January }, Enumerable.Empty<Guid>()
            , new List<TaxPrice>() { TaxPrice.Create("14:00", "15:00", 20) }, CityId);

        public static InitialSetting InitialSettingWithMaxNumber => InitialSetting.Create(true, Enumerable.Empty<DateTime>(), Enumerable.Empty<Month>(), Enumerable.Empty<Guid>()
            , new List<TaxPrice>() { TaxPrice.Create("14:00", "15:00", 80) }, CityId, 60);


    }
}
