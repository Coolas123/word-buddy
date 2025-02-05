using MassTransit.ExtensionsDependencyInjectionIntegration;

namespace MassTransit.Contracts
{
    public class MassTransitConfiguration
    {
        public bool IsDegub {  get; set; }

        public string ServiceName { get; set; }

        public Action<IBusRegistrationConfigurator> Configurator { get; set; }
    }
}
