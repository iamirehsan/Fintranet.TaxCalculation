namespace Fintranet.TaxCalculation.Service.ServiceInterfaces
{
    public interface IServiceHolder
    {
        public ITaxCalculator TaxCalculator { get; }
    }
}
