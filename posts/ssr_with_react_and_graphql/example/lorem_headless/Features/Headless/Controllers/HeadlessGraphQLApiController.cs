using GraphQL;
using GraphQL.NewtonsoftJson;
using GraphQL.Validation.Complexity;
using lorem_headless.Features.GraphQL.Models;
using lorem_headless.Features.Headless.Models;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace lorem_headless.Features.Headless.Controllers
{
    [RoutePrefix("api/headless")]
    public class HeadlessGraphQLApiController
        : ApiController
    {
        private readonly HeadlessSchema _schema;
        private readonly IDocumentExecuter _executer;
        private readonly IDocumentWriter _writer;

        public HeadlessGraphQLApiController(
            HeadlessSchema schema, 
            IDocumentExecuter executer, 
            IDocumentWriter writer)
        {
            _schema = schema;
            _executer = executer;
            _writer = writer;
        }

        [HttpPost, Route("graphql")]
        public async Task<HttpResponseMessage> PostAsync([FromBody] GraphQLQuery query)
        {
            var inputs = query.Variables.ToInputs();
            var queryToExecute = query.Query;

            var result = await _executer.ExecuteAsync(configure =>
            {
                configure.Schema = _schema;
                configure.Query = queryToExecute;
                configure.OperationName = query.OperationName;
                configure.Inputs = inputs;
                configure.ComplexityConfiguration = new ComplexityConfiguration { MaxDepth = 15 };

            }).ConfigureAwait(false);

            var httpResult = result.Errors?.Count > 0
                ? HttpStatusCode.BadRequest
                : HttpStatusCode.OK;

            var json = await _writer.WriteToStringAsync(result);

            var response = Request.CreateResponse(httpResult);
            response.Content = new StringContent(json, Encoding.UTF8, "application/json");

            return response;
        }
    }
}