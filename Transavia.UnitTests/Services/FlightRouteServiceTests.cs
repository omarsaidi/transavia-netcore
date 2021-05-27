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
    public class FlightRouteServiceTests
    {
        private readonly Mock<IHttpClientFactory> _httpClientFactory;
        private readonly Mock<ILogger<FlightRouteService>> _logger;

        public FlightRouteServiceTests()
        {
            _httpClientFactory = new Mock<IHttpClientFactory>();
            _logger = new Mock<ILogger<FlightRouteService>>();
        }
        [Fact]
        public async Task GetAllRoutesAsync_DataKO_ReturnsNull()
        {
            // Arrange
            Mock<HttpMessageHandler> handlerMock = new(MockBehavior.Strict);
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
            HttpClient client = new(handlerMock.Object)
            {
                BaseAddress = new Uri($"{Constants.TransaviaApiHost}/v3/routes")
            };

            _httpClientFactory.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(client);
            // Act       
            FlightRouteService flightRouteService = new(_httpClientFactory.Object, _logger.Object);
            FlightRoute[] flightRoutes = await flightRouteService.GetAllRoutesAsync(null);
            // Assert
            Assert.Null(flightRoutes);
        }
        [Fact]
        public async Task GetAllRoutesAsync_DataOK_ReturnsFlightOffers()
        {
            // Arrange
            Mock<HttpMessageHandler> handlerMock = new(MockBehavior.Strict);
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
                   Content = new StringContent(JsonConvert.SerializeObject(Array.Empty<FlightRoute>()), Encoding.UTF8, "application/json"),
               })
               .Verifiable();
            HttpClient client = new(handlerMock.Object)
            {
                BaseAddress = new Uri($"{Constants.TransaviaApiHost}/v3/routes")
            };

            _httpClientFactory.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(client);
            // Act       
            FlightRouteService flightRouteService = new(_httpClientFactory.Object, _logger.Object);
            FlightRoute[] flightRoutes = await flightRouteService.GetAllRoutesAsync(null);
            // Assert
            Assert.NotNull(flightRoutes);
        }
    }
}
