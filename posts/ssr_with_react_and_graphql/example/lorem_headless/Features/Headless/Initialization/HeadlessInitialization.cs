using EPiServer.Framework;
using EPiServer.Framework.Initialization;
using EPiServer.ServiceLocation;
using GraphQL;
using GraphQL.NewtonsoftJson;
using lorem_headless.Features.Headless.Models;

namespace lorem_headless.Features.Headless.Initialization
{
    [InitializableModule]
    public class HeadlessInitialization
        : IConfigurableModule
    {
        public void ConfigureContainer(ServiceConfigurationContext context)
        {
            context.Services.AddTransient<HeadlessQuery>();
            context.Services.AddSingleton<HeadlessSchema>();
        }

        public void Initialize(InitializationEngine context)
        {
        }

        public void Uninitialize(InitializationEngine context)
        {
        }
    }
}