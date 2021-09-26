using EPiServer.Core;
using EPiServer.ServiceLocation;
using GraphQL.Types;
using lorem_headless.Features.Breadcrumbs.Models;
using lorem_headless.Features.Breadcrumbs.Services;
using lorem_headless.Features.Navigation.Models;
using lorem_headless.Features.Navigation.Services;
using lorem_headless.Models.Pages;

namespace lorem_headless.Features.Headless.Models
{
    public class HeadlessQuery
        : ObjectGraphType<object>
    {
        public HeadlessQuery()
        {
            Name = "Query";

            Field<ModelTypeType>(
                "modelType",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "url" }
                ),
                resolve: context => {
                    return new ModelType();
                }
            );

            Field<StartPageModelType>(
                "startPage",
                resolve: context => {
                    return new StartPageModel();
                }
            );

            Field<ArticlePageModelType>(
                "article", 
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "pageId" }
                ),
                resolve: context => {
                    return new ArticlePageModel();
                }
            );

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

                    if(context.Arguments.ContainsKey("fromPageId") 
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