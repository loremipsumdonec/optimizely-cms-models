using GraphQL.Types;

namespace lorem_headless.Features.Headless.Models
{
    public class ContextModelType
        : ObjectGraphType<ContextModel>
    {
        public ContextModelType()
        {
            Field(m => m.PageId);
            Field(m => m.Url);
            Field(m => m.ModelType);
            Field(m => m.State);
        }
    }
}