using GraphQL.Types;

namespace lorem_headless.Features.Headless.Models
{
    public class ModelTypeType
        : ObjectGraphType<ModelType>
    {
        public ModelTypeType()
        {
            Field(m => m.Url);
            Field(m => m.Type);
        }
    }
}