using Fintranet.TaxCalculation.Infrastructure.Enum;
using Fintranet.TaxCalculation.Infrastructure.Exception;
using Fintranet.TaxCalculation.Message.Commands;
using Fintranet.TaxCalculation.Model.Entities.Base;
using Fintranet.TaxCalculation.Repository;
using Fintranet.TaxCalculation.Repository.RepositoryInterfaces;
using Fintranet.TaxCalculation.Service.Serviceimplementations;
using Fintranet.TaxCalculation.Test.TestData;
using Moq;
using System.Linq.Expressions;
namespace Fintranet.TaxCalculation.Test.Test.Service.Test
{

    public class TaxCalculatorTests
    {
        private Mock<IUnitOfWork> _unitOfWorkMock;
        private TaxCalculator _taxCalculator;

        public TaxCalculatorTests()
        {
            // Initialize mocks and the TaxCalculator instance before each test
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _taxCalculator = new TaxCalculator(_unitOfWorkMock.Object);
        }
        [Theory]
        [ClassData(typeof(TaxCalculatorCalculateCommandTestData))]
        public async Task Calculate_WhenInitialSettingIsIncomplete_ShouldReturnManagedException(CalculateCommand calculateCommand)
        {
       
            _unitOfWorkMock.Setup(repo => repo.VehicleRepository.Any(z => z.Id == calculateCommand.vehicleId)).Returns(true);
            _unitOfWorkMock.Setup(repo => repo.InitialSettingRepository.Any(z => z.CityId == calculateCommand.cityId)).Returns(false);

            var exception = await Assert.ThrowsAnyAsync<ManagedException>(async () => await _taxCalculator.Calculate(calculateCommand));

            Assert.Equal("No vehicle found with this id", exception.Message);
        }
        [Theory]
        [ClassData(typeof(TaxCalculatorCalculateCommandTestData))]
        public async Task Calculate_WhenVehicleDoesNotExist_ShouldReturnManagedException(CalculateCommand calculateCommand)
        {

            _unitOfWorkMock.Setup(repo => repo.InitialSettingRepository.Any(z => z.CityId == calculateCommand.cityId)).Returns(true);
            // Act & Assert
            var exception = await Assert.ThrowsAnyAsync<ManagedException>(async () => await _taxCalculator.Calculate(calculateCommand));

            Assert.Equal("Please complete initial setting first", exception.Message);
        }
        [Theory]
        [ClassData(typeof(TaxCalculatorCalculateCommandTestData))]
        public async Task Calculate_WhenVehicleIsTaxFree_ShouldReturnZero(CalculateCommand calculateCommand)
        {
           
            var vehicle = Vehicle.Create("bmw-i8", InitialTaxCalculatorData.FreeTaxVehicle);
            _unitOfWorkMock.Setup(repo => repo.InitialSettingRepository.FirstOrDefaultAsync(z => z.CityId == calculateCommand.cityId))
                                   .ReturnsAsync(InitialTaxCalculatorData.FreeTaxInitialSetting);
            _unitOfWorkMock.Setup(repo => repo.VehicleRepository.FirstOrDefaultAsync(z => z.Id == calculateCommand.vehicleId))
                      .ReturnsAsync(vehicle);

            var result = await _taxCalculator.Calculate(calculateCommand);

            Assert.Equal(result, 0);


        }
        [Theory]
        [ClassData(typeof(TaxCalculatorCalculateCommandTestData))]
        public async Task Calculate_WhenInitialSettingHasMaxTaxPerDay_ShouldReturnLessOrEqualThanMaxTaxPerDay(CalculateCommand calculateCommand)
        {

            _unitOfWorkMock.Setup(repo => repo.InitialSettingRepository.FirstOrDefaultAsync(z => z.CityId == calculateCommand.cityId))
                                  .ReturnsAsync(InitialTaxCalculatorData.InitialSettingWithMaxNumber);
            _unitOfWorkMock.Setup(repo => repo.VehicleRepository.FirstOrDefaultAsync(z => z.Id == calculateCommand.vehicleId))
                       .ReturnsAsync(InitialTaxCalculatorData.Vehicle);
            _unitOfWorkMock.Setup(repo => repo.VehicleTaxDateRepository.Where(z => z.VehicleId == calculateCommand.vehicleId && z.Time.Year.Equals(calculateCommand.year))).Returns(InitialTaxCalculatorData.VehicleTaxDates.AsQueryable());

            var result = await _taxCalculator.Calculate(calculateCommand);

            Assert.True(result <= InitialTaxCalculatorData.InitialSettingWithMaxNumber.MaximumTaxPerDay * InitialTaxCalculatorData.VehicleTaxDates.Count());


        }
        [Theory]
        [ClassData(typeof(TaxCalculatorCalculateCommandTestData))]
        public async Task Calculate_WithProperData_ShouldReturnProperResult(CalculateCommand calculateCommand)
        {

            _unitOfWorkMock.Setup(repo => repo.InitialSettingRepository.FirstOrDefaultAsync(z => z.CityId == calculateCommand.cityId))
                                    .ReturnsAsync(InitialTaxCalculatorData.InitialSettingWithoutMaxNumber);
            _unitOfWorkMock.Setup(repo => repo.VehicleRepository.FirstOrDefaultAsync(z => z.Id == calculateCommand.vehicleId))
                          .ReturnsAsync(InitialTaxCalculatorData.Vehicle);
            _unitOfWorkMock.Setup(repo => repo.VehicleTaxDateRepository.Where(z => z.VehicleId == calculateCommand.vehicleId && z.Time.Year.Equals(calculateCommand.year))).Returns(InitialTaxCalculatorData.VehicleTaxDates.AsQueryable());

            var result = await _taxCalculator.Calculate(calculateCommand);

            Assert.Equal(result,40);


        }
        [Theory]
        [ClassData(typeof(TaxCalculatorCalculateCommandTestData))]
        public async Task Calculate_WithTaxFreeMonth_ShouldReturnProperResult(CalculateCommand calculateCommand)
        {

            _unitOfWorkMock.Setup(repo => repo.InitialSettingRepository.FirstOrDefaultAsync(z => z.CityId == calculateCommand.cityId))
                                    .ReturnsAsync(InitialTaxCalculatorData.InitialSettingWithoutMaxNumberAndFreeTaxMonth);
            _unitOfWorkMock.Setup(repo => repo.VehicleRepository.FirstOrDefaultAsync(z => z.Id == calculateCommand.vehicleId))
                          .ReturnsAsync(InitialTaxCalculatorData.Vehicle);
            _unitOfWorkMock.Setup(repo => repo.VehicleTaxDateRepository.Where(z => z.VehicleId == calculateCommand.vehicleId && z.Time.Year.Equals(calculateCommand.year))).Returns(InitialTaxCalculatorData.VehicleTaxDates.AsQueryable());

            var result = await _taxCalculator.Calculate(calculateCommand);

            Assert.Equal(result, 20);


        }
    }
}
