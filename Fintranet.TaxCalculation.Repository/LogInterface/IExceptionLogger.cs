namespace Fintranet.TaxCalculation.Repository.LogInterface
{
    public interface IExceptionLogger
    {
        Task SetLog(string exception, string stackTrace);
    }
}
