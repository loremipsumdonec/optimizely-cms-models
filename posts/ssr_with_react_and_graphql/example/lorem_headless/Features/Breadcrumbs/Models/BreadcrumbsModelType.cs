using GraphQL.Types;

namespace lorem_headless.Features.Breadcrumbs.Models
{
    public class BreadcrumbsModelType 
        : ObjectGraphType<BreadcrumbsModel>
    {
        public BreadcrumbsModelType()
        {
            Field(m => m.Name);
            Field<ListGraphType<BreadcrumbType>>(
                "breadcrumbs", 
                resolve: context => context.Source.Breadcrumbs
            );
        }
    }
}