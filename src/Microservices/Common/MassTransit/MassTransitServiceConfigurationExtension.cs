using MassTransit.Contracts;
using Microsoft.Extensions.DependencyInjection;

namespace MassTransit.Configuration
{
    public static class MassTransitServiceConfigurationExtension
    {
        public static void Configure(
            this IServiceCollection services,
            Action<MassTransitConfiguration> configuration,
            string serviceName) {

            var transitConfiguration = new MassTransitConfiguration();

            if(configuration == null) {
                return;
            }

            configuration(transitConfiguration);

            if (string.IsNullOrWhiteSpace(serviceName)) {
                return;
            }

            transitConfiguration.ServiceName = serviceName;
            services.AddSingleton(transitConfiguration);
        }
    }
}
