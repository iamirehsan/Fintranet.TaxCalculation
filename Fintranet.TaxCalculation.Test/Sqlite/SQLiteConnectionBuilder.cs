using Fintranet.TaxCalculation.Repository.Implimentation;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace Fintranet.TaxCalculation.Test.Sqlite
{
    public static  class SQLiteConnectionBuilder
    {
        public static ApplicationDbContext CreateContext()
        {
            var connection = new SqliteConnection("Data Source=:memory:");
            connection.Open();

            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
                      .UseSqlite(connection);

            var dbContext = new ApplicationDbContext(optionsBuilder.Options);
            //dbContext.Database.Migrate();
            return dbContext;
        }
    }
}
