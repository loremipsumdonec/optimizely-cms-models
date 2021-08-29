using EPiServer.Framework;
using EPiServer.Framework.Initialization;
using lorem_headless.Features.Render.Services;
using System.Web.Mvc;

namespace lorem_headless.Features.Render.Initialization
{
    [InitializableModule]
    public class FilterProviderInitialization
        : IInitializableModule
    {
        public void Initialize(InitializationEngine context)
        {
            if (context.HostType == HostType.TestFramework)
            {
                return;
            }

            FilterProviders.Providers.Add(new RenderFilterProvider());
        }

        public void Uninitialize(InitializationEngine context)
        {
        }
    }
}