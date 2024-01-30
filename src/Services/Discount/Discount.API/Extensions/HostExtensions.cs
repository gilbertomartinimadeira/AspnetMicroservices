using Microsoft.Extensions.Logging;
using Npgsql;

namespace Discount.API.Extensions
{
    public static class HostExtensions
    {
        public static IHost MigrateDatabase<TContext>(this IHost host, int? retry = 0)
        {
            int retryForAvailability = retry.Value;
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var configuration = services.GetRequiredService<IConfiguration>();

                var logger = services.GetRequiredService<ILogger<TContext>>();

                try
                {
                    logger.LogInformation("Migrating postgres database.");

                    using var connection = new NpgsqlConnection(configuration["DatabaseSettings:ConnectionString"]);

                    connection.Open();

                    var command = new NpgsqlCommand{ Connection = connection};

                    logger.LogInformation("Dropping coupon table if exists.");

                    command.CommandText = "DROP TABLE IF EXISTS Coupon";
                    command.ExecuteNonQuery();

                    command.CommandText = @"CREATE TABLE Coupon (
                        Id SERIAL PRIMARY KEY,
                        ProductName VARCHAR(24) NOT NULL,
                        Description TEXT,
                        Amount INT
                    )";

                    logger.LogInformation("Recreating coupon table.");
                    command.ExecuteNonQuery();

                    command.CommandText =@"INSERT INTO Coupon (ProductName, Description, Amount) 
                                            VALUES ('IphoneX', 'IPhone Discount', 100)";

                    command.CommandText =@"INSERT INTO Coupon (ProductName, Description, Amount) 
                                            VALUES ('IphoneX', 'IPhone Discount', 230)";

                    logger.LogInformation("Seeding coupon table...");
                    
                    command.ExecuteNonQuery();

                    logger.LogInformation("Migration Done.");

                                               

                }
                catch (NpgsqlException ex)
                {
                    logger.LogError(ex,"An error occurred while migratin the pgsql database");

                    if(retryForAvailability < 50)
                    {
                        retryForAvailability++;
                        System.Threading.Thread.Sleep(2000);
                        MigrateDatabase<TContext>(host, retryForAvailability);
                    }
                    throw;
                }

            }


            return host;
        }
    }    
}
