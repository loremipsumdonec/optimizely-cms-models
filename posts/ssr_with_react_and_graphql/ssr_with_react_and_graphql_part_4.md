---
type: "chapter"
book: "/optimizely/ssr-with-react-and-graphql"
chapter: "/part-4"

title: "Adding support for GraphQL"
preamble: "Now it's time to add GraphQL support to an Optimizely CMS implementation."
preamble: "....."
In this example, the choice is to use [GraphQL.NET](https://graphql-dotnet.github.io/), if you are doing this with Optimizely CMS 12 you can also use [HotChocolate](https://github.com/ChilliCream/hotchocolate/). 

We will use GraphQL.NET together with WebApi that will be responsible for exposing an endpoint to GraphQL. It should be sufficient to install [GraphQL](https://www.nuget.org/packages/GraphQL/) and [GraphQL.NewtonsoftJson](https://www.nuget.org/packages/GraphQL.NewtonsoftJson/). In addition to this, we also add support WebApi and make it possible to use dependency injection for these controllers, see feature WebApi for example.

There are also some services for GraphQL.NET that are good to register for dependency injection and that is `DocumentExecuter` and `DocumentWriter`, see GraphQLInitialization.cs.

> Check that WebApi works by creating a simple controller that uses dependency injection.

## Create a schema

Everything that should be exposed with GraphQL needs to be defined as types and then registered in a schema. Which means that for every model that we are going to expose need a new class that defines the fields.

I will build extra classes for each type, so there will be significantly more code but this will make it is easier to show how it works.

> When I normally use GraphQL.NET I use a custom implementation that makes it possible to register various services such as `Register<GetBreadcrumbs, BreadcrumbsModel>()` and then all models are built or use what already exists.

### Breadcrumbs

A first feature that can be good to start with is support for breadcrumbs. For this we will need a service and two models. If you look at the files BreadcrumbsService.cs, BreadcrumbsModel.cs, Breadcrumb.cs, you can see the current implementation. The next step is to create the GraphQL types for the models and below you will see how these could be set up.

```csharp
public class BreadcrumbType
    : ObjectGraphType<Breadcrumb>
{
    public BreadcrumbType()
    {
   		Field(m => m.Text);
        Field(m => m.Url);
    }
}
```

```csharp
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
```

Before these become available via GraphQL, we need to add two more classes, one class that will be responsible for all queries and then schema. These classes should also be registered for dependency injection.

```csharp
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
```

```csharp
public class FirstIterationSchema
    : Schema
    {
        public FirstIterationSchema(FirstIterationQuery query)
        {
            Query = query;
        }
    }
```

The final step is then to create an endpoint, see FirstIterationApiController.cs for example. If you then visit the endpoint with GraphiQL, you should be able to use it.

![](./resources/first_iteration_breadcrumbs.png)



