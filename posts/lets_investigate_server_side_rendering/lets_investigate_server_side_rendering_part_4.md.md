---
type: "chapter"
book: "/optimizely/lets-investigate-server-side-rendering"
chapter: "/part-4"

title: "Build a single-page application"
preamble: "Now it's time to build a single-page application with React and Optimizely CMS."
---

## Frontend

The frontend will need a function to simulate an REST API, so we can start without any existing REST API in the backend. We don’t need any advanced function, so we can simulate it by adding json files in the _/public/api_ directory. Then we can call the REST API by adding _/api_ in the url and sending with `application/json` accepth header. The React application will also need to handle routing, state, stylesheets etc. and for this we will use the following.

- [react-router-dom](https://reactrouter.com/)
- [react-redux](https://react-redux.js.org/)
- [tailwindcss](https://tailwindcss.com/)
- [react-helmet](https://github.com/nfl/react-helmet)
- [axios](https://github.com/axios/axios)

### Routing

The routing will work according to the following logic: The application takes the current url and makes a call with same url under _/api_, for example: http://localhost:3000 will call http://localhost:3000/api/index.json and http://localhost:3000/startPage will call http://localhost:3000/api/startPage.json.

### Structure

The next thing to think about is how the data sent from the REST API will be structured, if we start with an `ArticlePage` that has the following properties: `Heading`, `Preamble`, and `Text`. Then it would be structured in this way.

```json
{
	"heading": "Fermentum ultrices lectus",
	"preamble": "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nulla posuere porttitor neque venenatis condimentum. Ut finibus aliquet odio, fermentum ultrices lectus",
	"text": "<h2>Porttitor neque venenatis</h2><p>Consectetur adipiscing elit. Nulla posuere porttitor neque venenatis condimentum. Ut finibus aliquet odio, fermentum ultrices lectus</p>"
}
```

But how do we know it's an `ArticlePage`? It does not appear anywhere in the json, so we need to extend it with a new property.

```json
{
	"heading": "Fermentum ultrices lectus",
	"preamble": "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nulla posuere porttitor neque venenatis condimentum. Ut finibus aliquet odio, fermentum ultrices lectus",
	"text": "<h2>Porttitor neque venenatis</h2><p>Consectetur adipiscing elit. Nulla posuere porttitor neque venenatis condimentum. Ut finibus aliquet odio, fermentum ultrices lectus</p>",
	"type": "ArticlePage"
}
```

Now it will be possible to identify what type of content is delivered.

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
                text: "Articles", 
                "url": "/articles"
            }
        ]
    },
    "breadcrumbs": {
        ...
    }
}
```