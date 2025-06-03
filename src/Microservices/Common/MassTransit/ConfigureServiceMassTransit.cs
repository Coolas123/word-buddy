using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MassTransit.Contracts
{
    public static class ConfigureServiceMassTransit
    {
        public static void ConfigureServices(
            IServiceCollection services, 
            IConfiguration configuration, 
            MassTransitConfiguration massTransitConfiguration) {

            if(massTransitConfiguration == null || massTransitConfiguration.IsDegub) {
                return;
            }

            var rabbitSection = configuration.GetSection("RabbitServer");

            var url = rabbitSection.GetValue<string>("Url");

            var host = rabbitSection.GetValue<string>("Host");

            if(rabbitSection == null || string.IsNullOrWhiteSpace(url) || string.IsNullOrWhiteSpace(host)) {
                throw new ArgumentNullException("no configuration data for RabbitMQ");
            }

            services.AddMassTransit(x => {
                x.UsingRabbitMq((busContext,busFactory) => {

                    busFactory.Host($"rabbitmq://{url}/{host}", configurator => {
                        configurator.Username("guest");
                        configurator.Password("guest");
                    });
                    busFactory.ClearSerialization();
                    busFactory.UseRawJsonSerializer();
                    busFactory.ConfigureEndpoints(busContext);
                });
                massTransitConfiguration.Configurator(x);
            });
        }
    }
}
