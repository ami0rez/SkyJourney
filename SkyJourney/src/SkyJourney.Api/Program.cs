using SkyJourney.Api.Utils.Configuration;
using SkyJourney.Api.Utils.Mapping;
using SkyJourney.Infrastructure.Utils;

const string DefaultCorsPolicy = "default";

var builder = WebApplication.CreateBuilder(args);

var appSettings = builder.Services.ConfigureAppsettings(builder.Configuration, "AppSettings");
builder.Services.AddCorsConfiguration(builder.Configuration);
builder.Services.ConfigureLogger();
builder.Services.ConfigureDatabase(appSettings.Database, appSettings.ConnectionString);

builder.Services.AddAutoMapper(typeof(SkyJourneyMappingProfile));
builder.Services.MapServicesToImplementations();

// Add services to the container.

builder.Services.AddControllers();


builder.Services.AddHttpContextAccessor();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.SeedTestData();

app.UseCorsSetup();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
