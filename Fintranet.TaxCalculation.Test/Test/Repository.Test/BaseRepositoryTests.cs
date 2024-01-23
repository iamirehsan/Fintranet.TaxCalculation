using Fintranet.TaxCalculation.Model.Entities.Base;
using Fintranet.TaxCalculation.Repository.Implimentation;
using Fintranet.TaxCalculation.Repository.Implimentation.RepositoryImplimentations;
using Fintranet.TaxCalculation.Repository.RepositoryInterfaces;
using Fintranet.TaxCalculation.Test.Sqlite;
using Fintranet.TaxCalculation.Test.TestData;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Fintranet.TaxCalculation.Test.Test.Repository.Test
{
    public class BaseRepositoryTests
    {
        private readonly ApplicationDbContext _contextMock;
        private readonly IVehicleRepository _repository;
        private readonly Vehicle _entity = InitialTaxCalculatorData.VehicleWithoutRelation;
        private readonly Vehicle _secondEntity = InitialTaxCalculatorData.SecondVehicleWithoutRelation;
        private readonly List<Vehicle> _entities;
        private readonly DbSet<Vehicle> _vehicles ;


        public BaseRepositoryTests()
        {
            _contextMock = SQLiteConnectionBuilder.CreateContext();
            _repository = new VehicleRepository(_contextMock);
            _vehicles = _contextMock.Set<Vehicle>();
            _entities = new List<Vehicle>() { _entity, _secondEntity };
        }

        [Fact]
        public async Task AddAsync_ShouldAddEntityInContext()
        {
            // Act
            await Add();

            // Assert
            Assert.True(_vehicles.Contains(_entity));
        }

        [Fact]
        public async Task AddRangeAsync_ShouldAddRageOfEntitiesInContext()
        {

            // Act
            await AddRange();
            var entities = _vehicles.Where(z => _entities.Contains(z));

            // Assert
            Assert.True(Enumerable.SequenceEqual(entities.OrderBy(t => t.Id), _entities.OrderBy(t => t.Id)));
        }

        [Fact]
        public async Task FindAsync_ShouldReturnCorrectEntity()
        {
            // Act
            await Add();
            var entity = await _repository.FindAsync(z => z.Name == _entity.Name);


            // Assert
            Assert.Equal(entity , new List<Vehicle>() { _entity });
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnAllEntites()
        {
            // Act
            await AddRange();
            var entities = await _repository.GetAllAsync();

            // Assert
            Assert.True(Enumerable.SequenceEqual(entities.OrderBy(t => t.Id), _entities.OrderBy(t => t.Id)));
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnOnlyExactEntity()
        {
            // Act
            await Add();
            var entitiy = await _repository.GetByIdAsync(_entity.Id);

            // Assert
            Assert.Equal(entitiy,_entity);
        }

        [Fact]
        public async Task Remove_ShouldRemoveExactEntity()
        {
            // Act
            await Add();
            _repository.Remove(_entity);
            await _contextMock.SaveChangesAsync();


            // Assert
            Assert.False(_vehicles.Contains(_entity));
        }

        [Fact]
        public async Task RemoveRange_ShouldRemoveExactRangeOfEntities()
        {
            // Act
            await AddRange();
            _repository.RemoveRange(_entities);
            await _contextMock.SaveChangesAsync();

            // Assert
            Assert.False(_vehicles.AsEnumerable().Intersect(_entities).Any());
        }
        [Fact]
        public async Task FirstOrDeafultAsync_ShouldReturnFirstElementOrNull()
        {
            // Act
            await AddRange();
            var entity = await  _repository.FirstOrDefaultAsync(z=>z.Name==_entity.Name);
            var secondEntity = await _repository.FirstOrDefaultAsync(z => z.Name == "noun");

            // Assert
            Assert.Equal(entity,_entity);
            Assert.Equal(secondEntity,null);
        }
        [Fact]
        public async Task OrderBy_ShouldReturnElementsInOrder()
        {
            await AddRange();
            var orderedEntites = await _repository.OrderBy(z => z.Name).ToListAsync();

            Assert.True(orderedEntites[0] == _entity && orderedEntites[1] == _secondEntity);
        }
        [Fact]
        public async Task OrderByDescending_ShouldReturnElementsInBackwardOrder()
        {
            await AddRange();
            var orderedEntites = await _repository.OrderByDescending(z => z.Name).ToListAsync();

            Assert.True(orderedEntites[1] == _entity && orderedEntites[0] == _secondEntity);

        }
        private async Task Add()
        {
            await _repository.AddAsync(_entity);
            await _contextMock.SaveChangesAsync();
        }
        private async Task AddRange()
        {
            await _repository.AddRangeAsync(_entities);
            await _contextMock.SaveChangesAsync();
        }
    }
}
