using Fintranet.TaxCalculation.Repository;
using Fintranet.TaxCalculation.Service.ServiceInterfaces;

namespace Fintranet.TaxCalculation.Service.Serviceimplementations
{
    public class ServiceHolder : IServiceHolder
    {
        private IUnitOfWork _unitOfWork;
        private TaxCalculator _taxCalculator;

        public ServiceHolder(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public ITaxCalculator TaxCalculator => _taxCalculator = _taxCalculator ?? new TaxCalculator(_unitOfWork);
    }
}
