using RestAssured.Logging;
using static RestAssured.Dsl;

namespace RestAssuredNet.Issue.Test
{
    public class WeatherForecastTests
    {
        LogConfiguration _logConfig;

        public WeatherForecastTests()
        {

            _logConfig = new LogConfiguration
            {
                RequestLogLevel = RequestLogLevel.All,
                ResponseLogLevel = ResponseLogLevel.All,
            };
        }

        [Fact]
        public void ShouldPostWeatherForecast()
        {
            string requestBody = $@"{{
                              ""Name"":""Stormy"",
                              ""TemperatureF"": 157
                            }}";

            Given()
                .Log(_logConfig)
                .ContentType("application/json")
                .Body(requestBody)
                .Post("http://localhost:5034/weatherforecast")
                .StatusCode(200);
        }
    }
}