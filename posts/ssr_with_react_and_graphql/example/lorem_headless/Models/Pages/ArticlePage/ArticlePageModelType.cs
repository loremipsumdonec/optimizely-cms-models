using GraphQL.Types;

namespace lorem_headless.Models.Pages
{
    public class ArticlePageModelType
        : ObjectGraphType<ArticlePageModel>
    {
        public ArticlePageModelType()
        {
            Field(m => m.Url);
            Field(m => m.Heading);
            Field(m => m.Preamble);
            Field(m => m.Text);
        }
    }
}