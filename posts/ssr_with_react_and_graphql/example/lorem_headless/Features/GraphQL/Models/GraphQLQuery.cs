using Newtonsoft.Json.Linq;

namespace lorem_headless.Features.GraphQL.Models
{
    public class GraphQLQuery
    {
        public string OperationName { get; set; }

        public string Query { get; set; }

        public JObject Variables { get; set; }
    }
}