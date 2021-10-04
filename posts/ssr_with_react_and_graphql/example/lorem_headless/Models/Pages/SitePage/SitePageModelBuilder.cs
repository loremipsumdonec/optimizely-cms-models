using EPiServer.Web.Routing;
using lorem_headless.Features.Headless.Services;

namespace lorem_headless.Models.Pages
{
    public class SitePageModelBuilder
        : ModelBuilder<SitePageModel, SitePage>
    {
        private readonly IUrlResolver _resolver;

        public SitePageModelBuilder(IUrlResolver resolver)
        {
            _resolver = resolver;
        }

        public override void Build(SitePageModel model, SitePage source)
        {
            model.SetLoadUrl(() => _resolver.GetUrl(source));
        }
    }
}