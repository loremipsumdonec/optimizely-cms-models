using EPiServer.Framework;
using EPiServer.Framework.Initialization;
using EPiServer.ServiceLocation;
using GraphQL;
using GraphQL.NewtonsoftJson;

namespace lorem_headless.Features.GraphQL.Initialization
{
    [InitializableModule]
    public class GraphQLInitialization
        : IConfigurableModule
    {
        public void ConfigureContainer(ServiceConfigurationContext context)
        {
            context.Services.AddSingleton<IDocumentExecuter>((_) => new DocumentExecuter());
            context.Services.AddSingleton<IDocumentWriter>((_) => new DocumentWriter(true));
        }

        public void Initialize(InitializationEngine context)
        {
        }

        public void Uninitialize(InitializationEngine context)
        {
        }
    }
}