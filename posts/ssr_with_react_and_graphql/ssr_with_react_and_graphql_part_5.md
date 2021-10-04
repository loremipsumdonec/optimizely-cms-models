---
type: "chapter"
book: "/optimizely/ssr-with-react-and-graphql"
chapter: "/part-5"

title: "An example"
preamble: "The example project has a feature that shows how to put together everything we have gone through."
---

This example has two page types and uses breadcrumbs. The project supports server-side rendering and then switches to a single-page application when loaded on the client side.

## An exercise

There is a function that is not implemented and that is navigation, which can be a good exercise if you want to test for yourself.

To get started check the configuration of the [Optimizely project](https://github.com/loremipsumdonec/optimizely-cms-models/tree/master/posts/ssr_with_react_and_graphql/example/lorem_headless) and then run the test case [CreateASite](https://github.com/loremipsumdonec/optimizely-cms-models/blob/master/posts/ssr_with_react_and_graphql/example/lorem_headless_tests/ExploratoryTests.cs), this will create a site with some pages. You can start the frontend project with `npm start`. After that, it's just to start building.

## Conclusion

Now I have shown how to go about creating a server-side rendered react application that uses GraphQL. The advantage of this approach is that the frontend has significantly more control over what data is needed and when it should be retrieved.
