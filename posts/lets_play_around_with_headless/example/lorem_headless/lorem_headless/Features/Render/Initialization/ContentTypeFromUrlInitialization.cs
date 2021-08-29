using EPiServer.Framework;
using EPiServer.Framework.Initialization;
using EPiServer.Web.Routing;
using System.Web;

namespace lorem_headless.Features.Render.Initialization
{
    [InitializableModule]
    public class ContentTypeFromUrlInitialization
        : IInitializableModule
    {
        public void Initialize(InitializationEngine context)
        {
            IContentRouteEvents contentRouteEvents = context.Locate.Advanced.GetInstance<IContentRouteEvents>();
            contentRouteEvents.RoutingContent += OnRoutingContent;
        }

        public void Uninitialize(InitializationEngine context)
        {
            IContentRouteEvents contentRouteEvents = context.Locate.Advanced.GetInstance<IContentRouteEvents>();
            contentRouteEvents.RoutingContent -= OnRoutingContent;
        }

        private void OnRoutingContent(object sender, RoutingEventArgs @event)
        {
            var context = @event.RoutingSegmentContext;
            string contentType = GetMIMETypeFromExtension(context.RemainingPath);

            if (!string.IsNullOrEmpty(contentType))
            {
                HttpContext.Current.Request.ContentType = contentType;
                context.RemainingPath = RemoveExtensionFromUrl(context.RemainingPath);
            }
            else if (UseFallbackIfMissingContentTypeAndAcceptType()
                || UseFallbackIfMissingContentTypeAndAcceptTypeIsWildcard())
            {
                HttpContext.Current.Request.ContentType = "text/html";
            }
            else if (string.IsNullOrEmpty(HttpContext.Current.Request.ContentType))
            {
                HttpContext.Current.Request.ContentType = HttpContext.Current.Request.AcceptTypes[0];
            }
        }

        private static bool UseFallbackIfMissingContentTypeAndAcceptTypeIsWildcard()
        {
            return string.IsNullOrEmpty(HttpContext.Current.Request.ContentType) 
                && HttpContext.Current.Request.AcceptTypes.Length > 0 
                && HttpContext.Current.Request.AcceptTypes[0] == "*/*";
        }

        private static bool UseFallbackIfMissingContentTypeAndAcceptType()
        {
            return string.IsNullOrEmpty(HttpContext.Current.Request.ContentType) 
                && HttpContext.Current.Request.AcceptTypes == null;
        }

        private string RemoveExtensionFromUrl(string remainingPath)
        {
            return remainingPath.Substring(0, remainingPath.LastIndexOf("."));
        }

        private string GetMIMETypeFromExtension(string segment)
        {
            switch(GetExtension(segment)) 
            {
                case "json":
                    return "application/json";
            }

            return null;
        }

        private string GetExtension(string segment)
        {
            if (!string.IsNullOrEmpty(segment) && segment.Contains("."))
            {
                return segment.Substring(segment.LastIndexOf(".") + 1);
            }

            return string.Empty;
        }
    }
}