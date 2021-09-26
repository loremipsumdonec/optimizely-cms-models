using GraphQL.Types;

namespace lorem_headless.Features.Navigation.Models
{
    public class NavigationModelType
        : ObjectGraphType<NavigationModel>
    {
        public NavigationModelType()
        {
            Field(m => m.AccessibilityDescription);
            Field(m => m.OpenNavigationPaneLabel);
            Field(m => m.CloseNavigationPaneLabel);
            Field(m => m.OpenNavigationItemLabel);
            Field(m => m.CloseNavigationItemLabel);

            Field<ListGraphType<NavigationItemType>>(
                "items",
                resolve: context => context.Source.Items
            );
        }
    }
}