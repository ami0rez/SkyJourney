using Amirez.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using SkyJourney.Infrastructure.Seeders;

namespace SkyJourney.Api.Utils.Configuration
{
    public static class DatabaseConfig
    {
        public static void SeedTestData(this IApplicationBuilder app)
        {
            using (var serviceScope = app?.ApplicationServices?.GetService<IServiceScopeFactory>()?.CreateScope())
            {
                if (serviceScope != null)
                {
                    serviceScope.ServiceProvider.GetRequiredService<DatabaseContext>().Database.Migrate();
                    var context = serviceScope.ServiceProvider.GetRequiredService<DatabaseContext>();
                    context.Database.EnsureCreated();
                    if (!context.Plans.Any())
                    {
                        var _seed = serviceScope.ServiceProvider.GetRequiredService<IDatabaseSeeder>();
                        _seed.GenerateCitiesAsync().Wait();
                        var plans = _seed.CreatePlan().Result;
                        foreach (var plan in plans)
                        {
                            var flights = _seed.GenerateFlightsAsync(plan, 10).Result;
                            foreach (var flight in flights)
                            {
                                _seed.CreateArrangement(flight).Wait();
                            }
                        }
                    }
                }
            }
        }
    }
}
