using Fintranet.TaxCalculation.Repository.Implimentation;
using Fintranet.TaxCalculation.Repository.Implimentation.RepositoryImplimentations;
using Fintranet.TaxCalculation.Repository.RepositoryInterfaces;
using Fintranet.TaxCalculation.Test.Sqlite;
using Fintranet.TaxCalculation.Test.TestData;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace Fintranet.TaxCalculation.Test.Test.Repository.Test
{
    public class BaseRepositoryTests
    {
        private readonly ApplicationDbContext _contextMock;
        private readonly IVehicleRepository _repository;

        public BaseRepositoryTests()
        {
            _contextMock = SQLiteConnectionBuilder.CreateContext();
            _repository = new VehicleRepository(_contextMock);
        }

        [Fact]
        public async Task AddAsync_ShouldAddEntityInContext()
        {
            
            var entity = InitialTaxCalculatorData.Vehicle;


            // Act
            await _repository.AddAsync(entity);

            // Assert

            Assert.True(_contextMock.Set<Vehicle>().Contains(entity));
        }
    }
}
