using EPiServer.Data;
using EPiServer.Framework;
using EPiServer.Framework.Initialization;
using lorem_headless.Features.Web.Services;
using System.Web.Mvc;

namespace lorem_headless.Features.Web.Initialization
{
    [InitializableModule]
    [ModuleDependency(typeof(DataInitialization))]
    public class WebInitialization
        : IInitializableModule
    {
        public void Initialize(InitializationEngine context)
        {
            DependencyResolver.SetResolver(
                new StructureMapWebDependencyResolver(context.Locate.Advanced.GetInstance<StructureMap.IContainer>())
            );
        }

        public void Uninitialize(InitializationEngine context)
        {
        }
    }
}