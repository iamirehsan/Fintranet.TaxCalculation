using Fintranet.TaxCalculation.Repository.Implimentation.RepositoryImplimentations;
using Fintranet.TaxCalculation.Repository.RepositoryInterfaces;

namespace Fintranet.TaxCalculation.Repository.Implimentation
{
    public class UnitOfWork : IUnitOfWork
    {
        private InitialSettingRepository _initialSettingRepository;
        private VehicleRepository _vehicleRepository;
        private VehicleTypeRepository _vehicleTypeRepository;
        private VehicleTaxDateRepository _vehicleTaxDateRepository;
        private CityRepository _cityRepository;


        private readonly ApplicationDbContext _context;
        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }


        public IInitialSettingRepository InitialSettingRepository => _initialSettingRepository = _initialSettingRepository ?? new InitialSettingRepository(_context);

        public IVehicleRepository VehicleRepository => _vehicleRepository = _vehicleRepository ?? new VehicleRepository(_context);

        public IVehicleTypeRepository VehicleTypeRepository => _vehicleTypeRepository = _vehicleTypeRepository ?? new VehicleTypeRepository(_context);

        public ICityRepository City => _cityRepository = _cityRepository ?? new CityRepository(_context);

        public IVehicleTaxDateRepository VehicleTaxDateRepository => _vehicleTaxDateRepository = _vehicleTaxDateRepository ?? new VehicleTaxDateRepository(_context);



        public void Dispose()
        {
            _context.Dispose();
        }

        public async Task CommitAsync()
        {
            await _context.SaveChangesAsync();
        }
        public void Commit()
        {
            _context.SaveChanges();
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }


    }
}

