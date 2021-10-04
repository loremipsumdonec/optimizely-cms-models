using lorem_headless.Features.Headless.Services;

namespace lorem_headless.Models.Pages
{
    public class ArticlePageModelBuilder
        : ModelBuilder<ArticlePageModel, ArticlePage>
    {
        public override void Build(ArticlePageModel model, ArticlePage source)
        {
            model.Heading = source.Heading;
            model.Preamble = source.Preamble;
            model.Text = source.Text?.ToString() ?? string.Empty;
        }
    }
}