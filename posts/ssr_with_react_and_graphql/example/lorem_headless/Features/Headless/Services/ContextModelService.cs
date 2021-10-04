using EPiServer;
using EPiServer.Core;
using EPiServer.Web.Routing;
using lorem_headless.Features.Headless.Models;
using lorem_headless.Models.Pages;

namespace lorem_headless.Features.Headless.Services
{
    public class ContextModelService
    {
        private readonly IUrlResolver _resolver;

        public ContextModelService(IUrlResolver resolver)
        {
            _resolver = resolver;
        }

        public ContextModel GetContextModel(string url) 
        {
            var content = _resolver.Route(new UrlBuilder(url));

            if(content == null || Exclude(content))
            {
                return new ContextModel(ContextModelState.NotFound);
            }

            return new ContextModel(ContextModelState.Found)
            {
                PageId = content.ContentLink.ID,
                Url = url,
                ModelType = GetModelType(content)
            };
        }

        private bool Exclude(IContent content) 
        {
            return !(content is PageData) || 
                !(content is SitePage);
        }

        private string GetModelType(IContent content) 
        {
            if(content is StartPage)
            {
                return nameof(StartPage);
            }
            else if(content is ArticlePage) 
            {
                return nameof(ArticlePage);
            }

            return "NotImplemented";
        }
    }
}