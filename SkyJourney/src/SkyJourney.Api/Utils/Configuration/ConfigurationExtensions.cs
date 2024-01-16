using Common.Configuration;
using Common.Logger;

namespace SkyJourney.Api.Utils.Configuration
{
    public static class ConfigurationExtensions
    {
        /// <summary>
        /// App settings configuration
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <param name="appsettingsKey"></param>
        /// <returns></returns>
        public static AppSettings ConfigureAppsettings(this IServiceCollection services, IConfiguration configuration, string appsettingsKey)
        {
            var appSettingsSection = configuration.GetSection(appsettingsKey);
            services.Configure<AppSettings>(appSettingsSection);
            var appSettings = appSettingsSection.Get<AppSettings>();
            return appSettings;
        }


        /// <summary>
        /// Logger Configuartion
        /// </summary>
        /// <param name="services"></param>
        public static void ConfigureLogger(this IServiceCollection services)
        {
            services.AddSingleton<ILoggerManager, LoggerManager>();
        }

        public static void AddCorsConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            // Bind CORS configuration from appsettings.json
            var corsConfig = configuration.GetSection("CorsPolicy").Get<CorsConfiguration>();
            // cors
            services.AddCors(options =>
            {
                // this defines a CORS policy called "default"
                options.AddPolicy("default", policy =>
                {
                    policy.WithOrigins(corsConfig.AllowedOrigins)
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
            });
        }
        public static void UseCorsSetup(this IApplicationBuilder app)
        {
            if (app == null) throw new ArgumentNullException(nameof(app));

            app.UseCors("default");
        }

    }
}
