using EPiServer.Core;
using EPiServer.ServiceLocation;
using GraphQL.Types;
using lorem_headless.Features.Breadcrumbs.Models;
using lorem_headless.Features.Breadcrumbs.Services;

namespace lorem_headless.Features.FirstIteration.Models
{
    public class FirstIterationQuery
        : ObjectGraphType<object>
    {
        public FirstIterationQuery()
        {
            Field<BreadcrumbsModelType>(
                "breadcrumbs",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<IntGraphType>> { Name = "forPageId" }
                ),
                resolve: context =>
                {
                    int forPageId = (int)context.Arguments["forPageId"].Value;
                    var forPage = new ContentReference(forPageId);

                    var service = ServiceLocator.Current.GetInstance<BreadcrumbsService>();
                    return service.GetBreadcrumbs(forPage);
                }
            );
        }
    }
}