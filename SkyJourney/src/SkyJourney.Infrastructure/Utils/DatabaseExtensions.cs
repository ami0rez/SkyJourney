using Amirez.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace SkyJourney.Infrastructure.Utils
{
    public static class DatabaseExtensions
    {
        public static void ConfigureDatabase(this IServiceCollection services, string databaseName, string connectionString)
        {
            switch (databaseName)
            {
                case "SQLite":
                    ConfigureSQLiteDatabase(services, connectionString);
                    break;
                case "Postgres":
                    // Configure Postgres Database
                    break;
                case "MySql":
                    // Configure MySql Database
                    break;
            }
        }


        /// <summary>
        /// Configures SQLite databae context
        /// </summary>
        /// <param name="services"></param>
        /// <param name="connectionstring"></param>
        public static void ConfigureSQLiteDatabase(this IServiceCollection services, string databaseName)
        {
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);
            var dbPath = Path.Join(path, databaseName);


            services.AddDbContext<DatabaseContext>(options =>
               options.UseSqlite($"Data Source={databaseName}")
               .UseSnakeCaseNamingConvention()
               );
        }
    }
}
