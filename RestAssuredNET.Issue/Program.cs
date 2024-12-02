using Microsoft.EntityFrameworkCore;
using RestAssuredNET.Issue;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<ForecastDb>(opt => opt.UseInMemoryDatabase("Forecasts"));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast = Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        {
            Name = $"{index}",
            TemperatureF = new Random().Next(-1000, 1000),
        })
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast");

app.Use(async (context, next) =>
{
    if (context.Request.ContentType != "application/json") throw new BadHttpRequestException("Only raw json requests are accepted");
    await next(context);
});

app.MapPost("/weatherforecast", CreateForecast);

app.Run();

static async Task<IResult> CreateForecast(WeatherForecast forecast, ForecastDb db)
{
    db.Forecasts.Add(forecast);
    await db.SaveChangesAsync();
    return TypedResults.Created($"/weatherforecast/{forecast}", forecast);
}


