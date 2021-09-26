using EPiServer;
using EPiServer.Core;
using EPiServer.ServiceLocation;
using EPiServer.Web.Routing;
using lorem_headless.Features.Navigation.Models;
using lorem_headless.Models.Pages;
using System.Collections.Generic;
using System.Linq;

namespace lorem_headless.Features.Navigation.Services
{
    [ServiceConfiguration(
        ServiceType = typeof(NavigationService),
        Lifecycle = ServiceInstanceScope.Transient)]
    public class NavigationService
    {
        private readonly IContentLoader _loader;
        private readonly IUrlResolver _resolver;
        private readonly SettingsPage _settingsPage;

        public NavigationService(
            IContentLoader loader, 
            IUrlResolver resolver, 
            SettingsPage settingsPage)
        {
            _loader = loader;
            _resolver = resolver;
            _settingsPage = settingsPage;
        }

        public NavigationModel GetNavigation(ContentReference fromPage) 
        {
            var model = new NavigationModel(() => GetNavigationItems(fromPage));
            model.OpenNavigationPaneLabel = _settingsPage?.NavigationSettings?.OpenNavigationPaneLabel;
            model.CloseNavigationPaneLabel = _settingsPage?.NavigationSettings?.CloseNavigationPaneLabel;
            model.OpenNavigationItemLabel = _settingsPage?.NavigationSettings?.OpenNavigationItemLabel;
            model.CloseNavigationItemLabel = _settingsPage?.NavigationSettings?.CloseNavigationItemLabel;
            model.AccessibilityDescription = _settingsPage?.NavigationSettings?.AccessibilityDescription;

            return model;
        }

        public IEnumerable<NavigationItem> GetNavigationItems(ContentReference from) 
        {
            foreach(var child in GetNavigationPages(from)) 
            {
                var item = CreateNavigationItem(child);
                
                if(item != null)
                {
                    yield return item;
                }
            }
        }

        private NavigationItem CreateNavigationItem(PageData page) 
        {
            if(page is SitePage sitePage)
            {
                return new NavigationItem(() => GetNavigationItems(page.ContentLink))
                {
                    Text = sitePage.Heading,
                    Url = _resolver.GetUrl(sitePage)
                };
            }

            return null;
        }

        private IEnumerable<SitePage> GetNavigationPages(ContentReference startPage)
        {
            return _loader.GetChildren<SitePage>(startPage)
                .Where(page => page.VisibleInMenu);
        }
    }
}