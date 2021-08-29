---
type: "chapter"
book: "/optimizely/lets-play-around-with-headless"
chapter: "/part-3"

title: "Architecture"
preamble: "Now it's getting closer to starting to implement some type of JavaScript rendering in Optimizely CMS. But before that, we need to make some architectural decisions."
---

Backend should not have to think about how the content is rendered, only what data should be sent to the view. Backend should be able to work in the same way, regardless of whether it is Razor view or React.

Backend should not send any IContent to the view, every content must be translated into a view model that can be serialized.

> If you try to serialize a for example a page, you will get the following error `Newtonsoft.Json.JsonSerializationException : Self referencing loop detected for property 'Parent..`

Backend needs to build the complete view model before sending it to the JavaScript engine. It’s “not possible” for the JavaScript engine to ask for more information, like PartialView found in MVC. 

## Some new features

There are some base features in the project lorem_headless, and I will not go into depth for these. The project has [extended the routing](ContentTypeFromUrlInitialization.cs) so that it is possible to write http://localhost:59590/index.json to get the content in json.  The project also uses an [IFilterProvider](DefaultFilterProvider.cs) to be able to add filters in a request based on some business logic.