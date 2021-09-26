
using GraphQL.Types;

namespace lorem_headless.Features.Breadcrumbs.Models
{
    public class BreadcrumbType
        : ObjectGraphType<Breadcrumb>
    {
        public BreadcrumbType()
        {
            Field(m => m.Text);
            Field(m => m.Url);
        }
    }
}