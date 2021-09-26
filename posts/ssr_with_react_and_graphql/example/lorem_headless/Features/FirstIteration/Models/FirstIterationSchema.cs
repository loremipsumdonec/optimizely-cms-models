using GraphQL.Types;

namespace lorem_headless.Features.FirstIteration.Models
{
    public class FirstIterationSchema
        : Schema
    {
        public FirstIterationSchema(FirstIterationQuery query)
        {
            Query = query;
        }
    }
}