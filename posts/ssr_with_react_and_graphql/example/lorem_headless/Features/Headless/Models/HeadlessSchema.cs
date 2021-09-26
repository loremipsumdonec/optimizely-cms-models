using GraphQL.Types;

namespace lorem_headless.Features.Headless.Models
{
    public class HeadlessSchema
        : Schema
    {
        public HeadlessSchema(HeadlessQuery query)
        {
            Query = query;
        }
    }
}