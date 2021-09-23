
namespace lorem_headless.Models.Pages
{
    public class ArticlePageModel
    {
        public ArticlePageModel(ArticlePage articlePage)
        {
            Heading = articlePage.Heading;
            Preamble = articlePage.Preamble;
            Text = articlePage.Text?.ToHtmlString();
        }

        public string Url { get; set; }

        public string Heading { get; set; }

        public string Preamble { get; set; }

        public string Text { get; set; }

        public string ModelType { get; } = nameof(ArticlePage);
    }
}