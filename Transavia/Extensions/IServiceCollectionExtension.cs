using System;
using Microsoft.Extensions.DependencyInjection;
using Transavia.Services;

namespace Transavia.Extensions
{
    public static class IServiceCollectionExtension
    {
        public static IServiceCollection AddTransaviaServices(this IServiceCollection services, bool isProd, string transaviaApiKey)
        {
            services.AddScoped<IFlightOfferService, FlightOfferService>();
            services.AddHttpClient("TRANSAVIA_API_HOST", c =>
            {
                c.BaseAddress = new Uri(isProd ? "https://api.transavia.com" : "https://tst.api.transavia.com");
                c.DefaultRequestHeaders.Add("apikey", transaviaApiKey);
            }).AddHttpMessageHandler(provider => RetryHandler.WithoutInnerHandler());
            return services;
        }
    }
}
