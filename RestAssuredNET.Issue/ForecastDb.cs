using Microsoft.EntityFrameworkCore;
using RestAssuredNET.Issue;

public class ForecastDb : DbContext
{
    public ForecastDb(DbContextOptions<ForecastDb> options)
        : base(options) { }

    public DbSet<WeatherForecast> Forecasts => Set<WeatherForecast>();
}