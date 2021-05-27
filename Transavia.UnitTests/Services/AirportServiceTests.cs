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
        #region GetAirportsByAirportAsync -----------------------------------
        [Fact]
        public async Task GetAirportsByAirportAsync_DataKO_ReturnsNull()
        {
            // Arrange
            string iataCode = "DJE";
            Mock<HttpMessageHandler> handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
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
                BaseAddress = new Uri($"{Constants.TransaviaApiHost}/v2/airports/nearest/{iataCode}")
            };

            _httpClientFactory.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(client);
            // Act       
            AirportService airportService = new(_httpClientFactory.Object, _logger.Object);
            Airport[] airports = await airportService.GetAirportsByAirportAsync(iataCode, null);
            // Assert
            Assert.Null(airports);
        }
        [Fact]
        public async Task GetAirportsByAirportAsync_DataOK_ReturnsAirports()
        {
            // Arrange
            string iataCode = "DJE";
            Mock<HttpMessageHandler> handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
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
                BaseAddress = new Uri($"{Constants.TransaviaApiHost}/v2/airports/nearest/{iataCode}")
            };

            _httpClientFactory.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(client);
            // Act       
            AirportService airportService = new(_httpClientFactory.Object, _logger.Object);
            Airport[] airports = await airportService.GetAirportsByAirportAsync(iataCode, null);
            // Assert
            Assert.NotNull(airports);
        }
        #endregion

        #region GetAirportsByCountryCodeAsync -------------------------------
        [Fact]
        public async Task GetAirportsByCountryCodeAsync_DataKO_ReturnsNull()
        {
            // Arrange
            string countryCode = "TUN";
            Mock<HttpMessageHandler> handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
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
                BaseAddress = new Uri($"{Constants.TransaviaApiHost}/v2/airports/countryCode/{countryCode}")
            };

            _httpClientFactory.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(client);
            // Act       
            AirportService airportService = new(_httpClientFactory.Object, _logger.Object);
            Airport[] airports = await airportService.GetAirportsByCountryCodeAsync(countryCode);
            // Assert
            Assert.Null(airports);
        }
        [Fact]
        public async Task GetAirportsByCountryCodeAsync_DataOK_ReturnsAirports()
        {
            // Arrange
            string countryCode = "TUN";
            Mock<HttpMessageHandler> handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
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
                BaseAddress = new Uri($"{Constants.TransaviaApiHost}/v2/airports/countryCode/{countryCode}")
            };

            _httpClientFactory.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(client);
            // Act       
            AirportService airportService = new(_httpClientFactory.Object, _logger.Object);
            Airport[] airports = await airportService.GetAirportsByCountryCodeAsync(countryCode);
            // Assert
            Assert.NotNull(airports);
        }
        #endregion

        #region GetAirportsByGeoCoordinatesAsync ----------------------------
        [Fact]
        public async Task GetAirportsByGeoCoordinatesAsync_DataKO_ReturnsNull()
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
                BaseAddress = new Uri($"{Constants.TransaviaApiHost}/v2/airports/nearest")
            };

            _httpClientFactory.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(client);
            // Act       
            AirportService airportService = new(_httpClientFactory.Object, _logger.Object);
            Airport[] airports = await airportService.GetAirportsByGeoCoordinatesAsync(null);
            // Assert
            Assert.Null(airports);
        }
        [Fact]
        public async Task GetAirportsByGeoCoordinatesAsync_DataOK_ReturnsAirports()
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
                   Content = new StringContent(JsonConvert.SerializeObject(Array.Empty<Airport>()), Encoding.UTF8, "application/json"),
               })
               .Verifiable();
            HttpClient client = new(handlerMock.Object)
            {
                BaseAddress = new Uri($"{Constants.TransaviaApiHost}/v2/airports/nearest")
            };

            _httpClientFactory.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(client);
            // Act       
            AirportService airportService = new(_httpClientFactory.Object, _logger.Object);
            Airport[] airports = await airportService.GetAirportsByGeoCoordinatesAsync(null);
            // Assert
            Assert.NotNull(airports);
        }
        #endregion

        #region GetAirportsByIdAsync ----------------------------------------
        [Fact]
        public async Task GetAirportsByIdAsync_DataKO_ReturnsNull()
        {
            // Arrange
            string iataCode = "DJE";
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
                BaseAddress = new Uri($"{Constants.TransaviaApiHost}/v2/airports/{iataCode}")
            };

            _httpClientFactory.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(client);
            // Act       
            AirportService airportService = new(_httpClientFactory.Object, _logger.Object);
            Airport airport = await airportService.GetAirportsByIdAsync(iataCode);
            // Assert
            Assert.Null(airport);
        }
        [Fact]
        public async Task GetAirportsByIdAsync_DataOK_ReturnsAirport()
        {
            // Arrange
            string iataCode = "DJE";
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
                   Content = new StringContent(JsonConvert.SerializeObject(new Airport()), Encoding.UTF8, "application/json"),
               })
               .Verifiable();
            HttpClient client = new(handlerMock.Object)
            {
                BaseAddress = new Uri($"{Constants.TransaviaApiHost}/v2/airports/{iataCode}")
            };

            _httpClientFactory.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(client);
            // Act       
            AirportService airportService = new(_httpClientFactory.Object, _logger.Object);
            Airport airport = await airportService.GetAirportsByIdAsync(iataCode);
            // Assert
            Assert.NotNull(airport);
        }
        #endregion

        #region GetAllAirportsAsync -----------------------------------------
        [Fact]
        public async Task GetAllAirportsAsync_DataKO_ReturnsNull()
        {
            // Arrange
            Mock<HttpMessageHandler> handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
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
                BaseAddress = new Uri($"{Constants.TransaviaApiHost}/v2/airports")
            };

            _httpClientFactory.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(client);
            // Act       
            AirportService airportService = new(_httpClientFactory.Object, _logger.Object);
            Airport[] airports = await airportService.GetAllAirportsAsync();
            // Assert
            Assert.Null(airports);
        }
        [Fact]
        public async Task GetAllAirportsAsync_DataOK_ReturnsAirports()
        {
            // Arrange
            Mock<HttpMessageHandler> handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
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
                BaseAddress = new Uri($"{Constants.TransaviaApiHost}/v2/airports")
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
