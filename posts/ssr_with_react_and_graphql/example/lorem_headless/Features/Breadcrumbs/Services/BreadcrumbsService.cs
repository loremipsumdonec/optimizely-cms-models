using EPiServer;
using EPiServer.Core;
using EPiServer.ServiceLocation;
using EPiServer.Web.Routing;
using lorem_headless.Features.Breadcrumbs.Models;
using lorem_headless.Models.Pages;
using System.Collections.Generic;

namespace lorem_headless.Features.Breadcrumbs.Services
{
    public class BreadcrumbsService
    {
        private readonly IContentLoader _loader;
        private readonly IUrlResolver _resolver;

        public BreadcrumbsService()
            : this(ServiceLocator.Current.GetInstance<IContentLoader>(), 
                  ServiceLocator.Current.GetInstance<IUrlResolver>()
                )
        {
        }

        public BreadcrumbsService(IContentLoader loader, IUrlResolver resolver)
        {
            _loader = loader;
            _resolver = resolver;
        }

        public BreadcrumbsModel GetBreadcrumbs(ContentReference forPage) 
        {
            var model = new BreadcrumbsModel();
            Stack<PageData> stack = new Stack<PageData>();
            stack.Push(_loader.Get<PageData>(forPage));

            while(stack.Count > 0) 
            {
                var current = stack.Pop();
                var breadcrumb = CreateBreadcrumb(current);
                model.Add(breadcrumb);

                if(current.ContentLink.Equals(ContentReference.StartPage))
                {
                    break;
                }

                var parent = _loader.Get<PageData>(current.ParentLink);
                stack.Push(parent);
            }

            return model;
        }

        private Breadcrumb CreateBreadcrumb(PageData page) 
        {
            if(IsPublished(page) && page is SitePage sitePage)
            {
                return new Breadcrumb()
                {
                    Text = sitePage.Heading,
                    Url = _resolver.GetUrl(sitePage)
                };
            }

            return null;
        }

        public bool IsPublished(PageData page)
        {
            return page.CheckPublishedStatus(PagePublishedStatus.Published) && !page.IsDeleted;
        }
    }
}