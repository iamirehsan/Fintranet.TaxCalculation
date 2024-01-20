using Fintranet.TaxCalculation.Infrastructure.Exception;

namespace Fintranet.TaxCalculation.Service.Helpers
{
    /// <summary>
    /// Exception Extentions :)
    /// </summary>
    public static class ValidateExtentions
    {
        public static void CheckShouldThrowNotFoundException(this object obj, string message)
        {
            if (obj is null)
            {
                throw new NotFoundException(message);
            }
        }

        public static void CheckShouldThrowNotFoundException(this IEnumerable<object> obj, string message)
        {
            if (obj.Count() == 0)
            {
                throw new NotFoundException(message);
            }
        }
    }
}
