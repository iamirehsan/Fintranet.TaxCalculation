using Fintranet.TaxCalculation.Repository.Implimentation;
using Microsoft.EntityFrameworkCore;
using Storm.PerformanceEvaluation.Repository.Implimentation.Log;

namespace Fintranet.TaxCalculation.Api.Base.Extentions
{
    public static class WebHostExtensions
    {
        public static WebApplication Seed(this WebApplication host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var serviceProvider = scope.ServiceProvider;
                var databaseContext = serviceProvider.GetRequiredService<ApplicationDbContext>();
                databaseContext.Database.Migrate();
                var logContext = serviceProvider.GetRequiredService<LogContext>();
                logContext.Database.Migrate();
            }

            return host;
        }
    }
}
