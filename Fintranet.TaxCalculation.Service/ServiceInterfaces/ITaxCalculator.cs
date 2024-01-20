using Fintranet.TaxCalculation.Message.Commands;

namespace Fintranet.TaxCalculation.Service.ServiceInterfaces
{
    public interface ITaxCalculator
    {
        Task<double> Calculate(CalculateCommand calculateCommand);
    }
}
