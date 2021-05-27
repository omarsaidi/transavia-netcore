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
    public class FlightOfferServiceTests
    {
        private readonly Mock<IHttpClientFactory> _httpClientFactory;
        private readonly Mock<ILogger<FlightOfferService>> _logger;

        public FlightOfferServiceTests()
        {
            _httpClientFactory = new Mock<IHttpClientFactory>();
            _logger = new Mock<ILogger<FlightOfferService>>();
        }
        [Fact]
        public async Task GetFlightOffersAsync_DataKO_ReturnsNull()
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
                BaseAddress = new Uri($"{Constants.TransaviaApiHost}/v1/flightoffers")
            };

            _httpClientFactory.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(client);
            // Act       
            FlightOfferService flightOfferService = new(_httpClientFactory.Object, _logger.Object);
            FlightOffersSerach flightOffers = await flightOfferService.GetFlightOffersAsync(null);
            // Assert
            Assert.Null(flightOffers);
        }
        [Fact]
        public async Task GetFlightOffersAsync_DataOK_ReturnsFlightOffers()
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
                   Content = new StringContent(JsonConvert.SerializeObject(Array.Empty<FlightOffer>()), Encoding.UTF8, "application/json"),
               })
               .Verifiable();
            HttpClient client = new(handlerMock.Object)
            {
                BaseAddress = new Uri($"{Constants.TransaviaApiHost}/v1/flightoffers")
            };

            _httpClientFactory.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(client);
            // Act       
            FlightOfferService flightOfferService = new(_httpClientFactory.Object, _logger.Object);
            FlightOffersSerach flightOffers = await flightOfferService.GetFlightOffersAsync(null);
            // Assert
            Assert.NotNull(flightOffers);
        }
    }
}
