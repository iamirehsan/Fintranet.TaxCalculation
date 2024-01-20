using Fintranet.TaxCalculation.Infrastructure.Enum;
using Fintranet.TaxCalculation.Model.Entities.Base;
using Fintranet.TaxCalculation.Service.ServiceInterfaces;
using Microsoft.EntityFrameworkCore;
using Fintranet.TaxCalculation.Repository;
using Fintranet.TaxCalculation.Infrastructure.Exception;
using Fintranet.TaxCalculation.Message.Commands;

namespace Fintranet.TaxCalculation.Service.Serviceimplementations
{
    public class TaxCalculator : ITaxCalculator
    {
        private IUnitOfWork _unitOfWork;

        public TaxCalculator(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<double> Calculate(CalculateCommand calculateCommand)
        {
            // Check if initial setting is incomplete
            if (_unitOfWork.InitialSettingRepository.Any(z => z.CityId == calculateCommand.cityId))
            {
                throw new ManagedException("Please complete initial setting first");
            }

            // Check if vehicle with the given id exists
            if (_unitOfWork.VehicleRepository.Any(z => z.Id == calculateCommand.vehicleId))
            {
                throw new ManagedException("No vehicle found with this id");
            }

            var initialSetting = await _unitOfWork.InitialSettingRepository
                .FirstOrDefaultAsync(z => z.CityId == calculateCommand.cityId)
                .ConfigureAwait(false);

            var vehicle = await _unitOfWork.VehicleRepository
                .FirstOrDefaultAsync(z => z.Id == calculateCommand.vehicleId)
                .ConfigureAwait(false);

            // Check if the vehicle is eligible for free tax
            if (initialSetting.FreeTaxVehicleTypes.Any(z => z==vehicle.VehicleTypeId))
                return 0;

            var vehicleTaxDates = _unitOfWork.VehicleTaxDateRepository
                .Where(z => z.VehicleId == calculateCommand.vehicleId && z.Time.Year.Equals(calculateCommand.year));

            var vehicleGroupsByDate =  vehicleTaxDates.GroupBy(z => z.Time.Date).ToList();

            double totalTax = 0;

            foreach (var dateGroup in vehicleGroupsByDate)
            {
                if (initialSetting.FreeTaxMonth.Contains((Month)dateGroup.First().Time.Month) || initialSetting.FreeTaxDates.Contains(dateGroup.First().Time.Date))
                       continue;
                   
                double tax = CalculateTaxForDateGroup(dateGroup, initialSetting);
                totalTax += tax;
            }

            return totalTax;
        }

        // Move this block of code into a separate method for better readability
        private double CalculateTaxForDateGroup(IEnumerable<VehicleTaxDate> dateGroup, InitialSetting initialSetting)
        {
            double tax = 0;
            foreach (var innerItem in dateGroup)
            {
                if (tax >= initialSetting.MaximumTaxPerDay)
                    break;

                tax += initialSetting.TaxPrices
                    .First(z => z.StartingTime.CompareTo(innerItem.TimeBaseOnHourAndMinuteAsString) <= 0 &&
                                z.EndingTime.CompareTo(innerItem.TimeBaseOnHourAndMinuteAsString) >= 0)
                    .Price;
            }

            return initialSetting.MaximumTaxPerDay is null ? tax : (tax > initialSetting.MaximumTaxPerDay.Value ? initialSetting.MaximumTaxPerDay.Value : tax);
        }
    }
}