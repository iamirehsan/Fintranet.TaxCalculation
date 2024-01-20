using Fintranet.TaxCalculation.Repository.RepositoryInterfaces;

namespace Fintranet.TaxCalculation.Repository
{
    public interface IUnitOfWork
    {
        public IInitialSettingRepository InitialSettingRepository { get; }
        public ICityRepository City { get; }
        public IVehicleTaxDateRepository VehicleTaxDateRepository { get; }
        public IVehicleRepository VehicleRepository { get; }
        public IVehicleTypeRepository VehicleTypeRepository { get; }
        public Task SaveAsync();
        public void Dispose();
        public Task CommitAsync();
        public void Commit();
    }
}
