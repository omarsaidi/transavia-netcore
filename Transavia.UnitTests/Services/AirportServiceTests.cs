using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moq;
using Moq.Protected;
using Newtonsoft.Json;
using Transavia.Resources;
using Transavia.Services;
using Xunit;

namespace Transavia.UnitTests.Services
{
    public class AirportServiceTests
    {
        private readonly Mock<IHttpClientFactory> _httpClientFactory;
        private readonly Mock<ILogger<AirportService>> _logger;

        public AirportServiceTests()
        {
            _httpClientFactory = new Mock<IHttpClientFactory>();
            _logger = new Mock<ILogger<AirportService>>();
        }
        #region GetAllAirportsAsync -----------------------------------------
        [Fact]
        public async Task GetAllAirportsAsync_DataKO_ReturnsNull()
        {
            // Arrange
            var handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            handlerMock
               .Protected()
               // Setup the PROTECTED method to mock
               .Setup<Task<HttpResponseMessage>>(
                  "SendAsync",
                  ItExpr.IsAny<HttpRequestMessage>(),
                  ItExpr.IsAny<CancellationToken>()
               )
               // prepare the expected response of the mocked http call
               .ReturnsAsync(new HttpResponseMessage()
               {
                   StatusCode = HttpStatusCode.BadRequest,
                   Content = new StringContent(string.Empty),
               })
               .Verifiable();
            HttpClient client = new HttpClient(handlerMock.Object)
            {
                BaseAddress = new Uri(Constants.TransaviaApiHost)
            };

            _httpClientFactory.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(client);
            // Act       
            AirportService airportService = new(_httpClientFactory.Object, _logger.Object);
            Airport[] airports = await airportService.GetAllAirportsAsync();
            // Assert
            Assert.Null(airports);
        }
        [Fact]
        public async Task GetAllAirportsAsync_DataOK_ReturnsFlightDestinations()
        {
            // Arrange
            var handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            handlerMock
               .Protected()
               // Setup the PROTECTED method to mock
               .Setup<Task<HttpResponseMessage>>(
                  "SendAsync",
                  ItExpr.IsAny<HttpRequestMessage>(),
                  ItExpr.IsAny<CancellationToken>()
               )
               // prepare the expected response of the mocked http call
               .ReturnsAsync(new HttpResponseMessage()
               {
                   StatusCode = HttpStatusCode.OK,
                   Content = new StringContent(JsonConvert.SerializeObject(Array.Empty<Airport>()), Encoding.UTF8, "application/json"),
               })
               .Verifiable();
            HttpClient client = new(handlerMock.Object)
            {
                BaseAddress = new Uri(Constants.TransaviaApiHost)
            };

            _httpClientFactory.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(client);
            // Act       
            AirportService airportService = new(_httpClientFactory.Object, _logger.Object);
            Airport[] airports = await airportService.GetAllAirportsAsync();
            // Assert
            Assert.NotNull(airports);
        }
        #endregion
    }
}
