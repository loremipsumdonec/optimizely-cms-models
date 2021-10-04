using EPiServer.Core;
using EPiServer.ServiceLocation;
using GraphQL.Types;
using lorem_headless.Features.Breadcrumbs.Models;
using lorem_headless.Features.Breadcrumbs.Services;
using lorem_headless.Features.Headless.Services;
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

            Field<ContextModelType>(
                "contextModel",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "url" }
                ),
                resolve: context => {

                    string url = (string)context.Arguments["url"].Value;

                    var service = ServiceLocator.Current.GetInstance<ContextModelService>();
                    return service.GetContextModel(url);
                }
            );

            Field<StartPageModelType>(
                "startPage",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<IntGraphType>> { Name = "id" }
                ),
                resolve: context => {
                    int id = (int)context.Arguments["id"].Value;
                    var service = ServiceLocator.Current.GetInstance<ContentService>();

                    return service.GetContent<StartPageModel>(
                        id, typeof(StartPageModelBuilder), typeof(SitePageModelBuilder)
                    );
                }
            );

            Field<ArticlePageModelType>(
                "articlePage", 
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<IntGraphType>> { Name = "id" }
                ),
                resolve: context => {
                    int id = (int)context.Arguments["id"].Value;
                    var service = ServiceLocator.Current.GetInstance<ContentService>();

                    return service.GetContent<ArticlePageModel>(
                        id, typeof(ArticlePageModelBuilder), typeof(SitePageModelBuilder)
                    );
                }
            );

            Field<NonNullGraphType<ListGraphType<NonNullGraphType<ArticlePageModelType>>>>(
                "articles",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<IntGraphType>> { Name = "parentId" }
                ),
                resolve: context => {
                    int id = (int)context.Arguments["parentId"].Value;
                    var service = ServiceLocator.Current.GetInstance<ContentService>();

                    return service.GetChildren<ArticlePage, ArticlePageModel>(
                        id, typeof(ArticlePageModelBuilder), typeof(SitePageModelBuilder)
                    );
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