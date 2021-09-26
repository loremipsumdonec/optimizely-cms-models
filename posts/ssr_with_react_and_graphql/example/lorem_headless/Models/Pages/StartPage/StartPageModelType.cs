using GraphQL.Types;

namespace lorem_headless.Models.Pages
{
    public class StartPageModelType
        : ObjectGraphType<StartPageModel>
    {
        public StartPageModelType()
        {
            Field(m => m.Url);
            Field(m => m.Heading);
            Field(m => m.Preamble);
        }
    }
}