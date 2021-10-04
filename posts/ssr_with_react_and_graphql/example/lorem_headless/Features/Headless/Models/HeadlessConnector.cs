using EPiServer.ServiceLocation;
using GraphQL;
using GraphQL.NewtonsoftJson;
using GraphQL.Validation.Complexity;
using lorem_headless.Features.GraphQL.Models;
using lorem_headless.Features.Render.Services;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace lorem_headless.Features.Headless.Models
{
    public class HeadlessConnector
        : Connector
    {
        private readonly HeadlessSchema _schema;
        private readonly IDocumentExecuter _executer;
        private readonly IDocumentWriter _writer;

        public HeadlessConnector() 
            : this(
                ServiceLocator.Current.GetInstance<HeadlessSchema>(),
                ServiceLocator.Current.GetInstance<IDocumentExecuter>(),
                ServiceLocator.Current.GetInstance<IDocumentWriter>()                
            )
        {
        }

        public HeadlessConnector(
            HeadlessSchema schema,
            IDocumentExecuter executer,
            IDocumentWriter writer)
        {
            _schema = schema;
            _executer = executer;
            _writer = writer;
        }

        public async override Task<string> Execute(string url, string query)
        {
            var q = JsonConvert.DeserializeObject<GraphQLQuery>(query);
            var inputs = q.Variables.ToInputs();
            var queryToExecute = q.Query;

            var result = await _executer.ExecuteAsync(configure =>
            {
                configure.Schema = _schema;
                configure.Query = queryToExecute;
                configure.OperationName = q.OperationName;
                configure.Inputs = inputs;
                configure.ComplexityConfiguration = new ComplexityConfiguration { MaxDepth = 15 };
            });

            return await _writer.WriteToStringAsync(result);
        }
    }
}