using GraphQL.Types;

namespace lorem_headless.Features.Navigation.Models
{
    public class NavigationItemType
        : ObjectGraphType<NavigationItem>
    {
        public NavigationItemType()
        {
            Field(m => m.Url);
            Field(m => m.Text);

            Field<ListGraphType<NavigationItemType>>(
                "items",
                resolve: context => context.Source.Items
            );
        }
    }
}