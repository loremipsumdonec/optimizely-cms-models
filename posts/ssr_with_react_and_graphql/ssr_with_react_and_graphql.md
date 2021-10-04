---
date: "2021-09-23"
type: "book"
book: "/optimizely/ssr-with-react-and-graphql"
theme: "red"
state: "done"
tags: ["optimizely"]

repository_url: "https://github.com/loremipsumdonec/optimizely-cms-models"
repository_name: "optimizely-cms-models"
repository_base: "https://github.com/loremipsumdonec/optimizely-cms-models/blob/master/posts/ssr_with_react_and_graphql"

title: "SSR with React and GraphQL"
preamble: "In my previous post, I showed how to get started with server-side rendering and React. Now I intend to show how to continue with this and add support for GraphQL, so frontend can control which data to get."


---

In the previous setup that I showed in the post [Server-side rendering with React](https://www.tiff.se/optimizely/lets-investigate-server-side-rendering), the backend delivered a complete model with all the data to the frontend for the current page. Which meant that the backend needed to know which features a page should use. This usually leads to little over-fetching. 

[GraphQL](https://graphql.org/) makes it possible for frontend to query the data needed, and this is more based on what components are used on the current page. Backend does not need to have some deeper knowledge of exactly what features a page should use, but can focus on building separate features. 

