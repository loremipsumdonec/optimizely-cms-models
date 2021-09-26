using EPiServer.Framework;
using EPiServer.Framework.Initialization;
using EPiServer.ServiceLocation;
using lorem_headless.Features.FirstIteration.Models;

namespace lorem_headless.Features.FirstIteration.Initialization
{
    [InitializableModule]
    public class FirstIterationInitialization
        : IConfigurableModule
    {
        public void ConfigureContainer(ServiceConfigurationContext context)
        {
            context.Services.AddTransient<FirstIterationQuery>();
            context.Services.AddSingleton<FirstIterationSchema>();
        }

        public void Initialize(InitializationEngine context)
        {
        }

        public void Uninitialize(InitializationEngine context)
        {
        }
    }
}