using EPiServer.Core;
using EPiServer.ServiceLocation;
using GraphQL.Types;
using lorem_headless.Features.Breadcrumbs.Models;
using lorem_headless.Features.Breadcrumbs.Services;
using lorem_headless.Features.Navigation.Models;
using lorem_headless.Features.Navigation.Services;

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

            Field<NavigationModelType>(
                "navigation",
                arguments: new QueryArguments(
                    new QueryArgument<IntGraphType> { Name = "fromPageId" }
                ),
                resolve: context =>
                {
                    ContentReference fromPage = ContentReference.StartPage;

                    if (context.Arguments.ContainsKey("fromPageId")
                        && context.Arguments["fromPageId"].Value != null)
                    {
                        fromPage = new ContentReference((int)context.Arguments["fromPageId"].Value);
                    }

                    var service = ServiceLocator.Current.GetInstance<NavigationService>();
                    return service.GetNavigation(fromPage);
                }
            );
        }
    }
}