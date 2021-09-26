using EPiServer;
using EPiServer.Core;
using EPiServer.Framework;
using EPiServer.Framework.Initialization;
using EPiServer.ServiceLocation;
using lorem_headless.Models.Pages;
using System.Linq;

namespace lorem_headless.Features.Settings.Initialization
{
    [InitializableModule]
    public class SettingsPageInitialization
        : IConfigurableModule
    {
        public void ConfigureContainer(ServiceConfigurationContext context)
        {
            if(context.HostType == HostType.TestFramework) 
            {
                return;
            }

            context.Services.AddHttpContextScoped((locator) => {
                var loader = locator.GetInstance<IContentLoader>();
                return loader.GetChildren<SettingsPage>(ContentReference.StartPage).FirstOrDefault();
            
            });
        }

        public void Initialize(InitializationEngine context)
        {
        }

        public void Uninitialize(InitializationEngine context)
        {
        }
    }
}