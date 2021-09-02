---
type: "chapter"
book: "/optimizely/lets-investigate-server-side-rendering"
chapter: "/part-4"

title: "Build a single-page application"
preamble: "Now it's time to build a single-page application with React and Optimizely CMS."
---

## Frontend

The frontend will need a function to simulate an REST API, so we can start without any existing REST API in the backend. We don’t need any advanced function, so we can simulate it by adding json files in the _/public/api_ directory. Then we can call the REST API by adding _/api_ in the url and sending with `application/json` accept header. The React application will also need to handle routing, state, stylesheets etc. and for this we will use the following.

- [react-router-dom](https://reactrouter.com/)
- [react-redux](https://react-redux.js.org/)
- [tailwindcss](https://tailwindcss.com/)
- [axios](https://github.com/axios/axios)

> The example project for frontend can be found [here](https://github.com/loremipsumdonec/optimizely-cms-models/tree/master/posts/lets_play_around_with_headless/example/lorem_headless_react_page)

### Routing

The routing in the frontend will work according to the following logic: The application takes the current url and makes a call with same url under _/api_, for example: http://localhost:3000 will call http://localhost:3000/api/index.json and http://localhost:3000/startPage will call http://localhost:3000/api/startPage.json.

### Structure

The next thing to think about is how the data sent from the REST API will be structured, if we start with an `ArticlePage` that has the following properties: `Heading`, `Preamble`, and `Text`. Then it would be structured in this way.

```json
{
	"heading": "Fermentum ultrices lectus",
	"preamble": "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nulla posuere porttitor neque venenatis condimentum. Ut finibus aliquet odio, fermentum ultrices lectus",
	"text": "<h2>Porttitor neque venenatis</h2><p>Consectetur adipiscing elit. Nulla posuere porttitor neque venenatis condimentum. Ut finibus aliquet odio, fermentum ultrices lectus</p>"
}
```

But how do we know it's an `ArticlePage`? It does not appear anywhere in the json, so we need to add a property.

```json
{
	"heading": "Fermentum ultrices lectus",
	"preamble": "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nulla posuere porttitor neque venenatis condimentum. Ut finibus aliquet odio, fermentum ultrices lectus",
	"text": "<h2>Porttitor neque venenatis</h2><p>Consectetur adipiscing elit. Nulla posuere porttitor neque venenatis condimentum. Ut finibus aliquet odio, fermentum ultrices lectus</p>",
	"modelType": "ArticlePage"
}
```

Now it will be possible to identify what type of content is delivered.

### We need more

A website has more features, such as a navigation, breadcrumbs etc. How should this be stored in the json? We need to change the structure of JSON to something like below.

```json
{
    "content": {
        "heading": "Fermentum ultrices lectus",
        "preamble": "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nulla posuere porttitor neque venenatis condimentum. Ut finibus aliquet odio, fermentum ultrices lectus",
        "text": "<h2>Porttitor neque venenatis</h2><p>Consectetur adipiscing elit. Nulla posuere porttitor neque venenatis condimentum. Ut finibus aliquet odio, fermentum ultrices lectus</p>",
        "type": "ArticlePage"
    },
    "navigation": {
        "items": [
            { 
                "text: "Articles", 
                "url": "/articles"
            }
        ]
    },
    "breadcrumbs": {
        ...
    }
}
```

### Rendering

In the previous examples we have used the function `ReactDOMServer.renderToStaticMarkup` but now when we are going to set up a single-page application we need to use `ReactDOMServer.renderToString` instead, and make sure that the same model is sent to the client. 

>  Another detail is that on the client side you need to use `ReactDOM.hydrate`, read more about this [here](https://reactjs.org/docs/react-dom.html#hydrate).

This is done by storing the model in the [HTML](). When react starts up on the client side, the application has access to the same data without having to make any extra API calls. Check out both the [server.js](https://github.com/loremipsumdonec/optimizely-cms-models/tree/master/posts/lets_play_around_with_headless/example/lorem_headless_react_page/src/server.js) and [index.js](https://github.com/loremipsumdonec/optimizely-cms-models/tree/master/posts/lets_play_around_with_headless/example/lorem_headless_react_page/src/index.js) files in the example project. 

To render a page, you need to link a component to `modelType`, see [ContentFactory.js](https://github.com/loremipsumdonec/optimizely-cms-models/tree/master/posts/lets_play_around_with_headless/example/lorem_headless_react_page/src/Components/ContentFactory/ContentFactory.js)

## Backend

The biggest difference in backend is that you cannot use partial views, the whole model needs to be built before it is sent to the JavaScript engine. Apart from that, there is no difference in how you work from with Razor.

> It depends more on what flexibility you want. If you use `IActionFilter`, it will be easier to expand without changing existing code, but it will be a little harder to get an overall picture and follow the application flow.

```csharp
public class StartPageController
	: PageController<StartPage>, IWebController
{
	...

    public ActionResult Index(StartPage currentPage) 
    {
        var model = new StartPageModel(currentPage)
        {
            Url = _resolver.GetUrl(currentPage)
        };

        LoadArticles(currentPage, model);

        return View(model);
    }
}

```

## Test yourself

If you want to test the application, you can find the example project here. Start by running the test case [CreateSiteWithCreateReactAppFinal](https://github.com/loremipsumdonec/optimizely-cms-models/tree/master/posts/lets_play_around_with_headless/example/lorem_headless/lorem_headless_tests/ExploratoryTests.cs#L35), this will create a database and add some content. After that, it should just be to run the application.

> As with all projects, always check the settings in Web.config before you start

## Conslusion

This is not a headless solution, what we have done is replace Razor with React. There are several benefits to this. It is easier to work in parallel and divide the work between different teams. It will also be easier to perform frontend testing.

There are also problems with the solution. You must be prepared to troubleshoot cryptic error messages that can occur at server-side rendering, you need to be familiar with how the application flow works from backend to frontend. You will also have to make sure that you keep important functions for the editors such as on-page-edit, blocks, Episerver Forms etc.

But once you have solved these problems and you become more familiar with the process, it is relatively easy to move the frontend to a completely separate application such as [Next.js](https://nextjs.org/).
