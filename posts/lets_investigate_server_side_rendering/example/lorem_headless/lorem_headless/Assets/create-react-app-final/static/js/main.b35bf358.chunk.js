(this.webpackJsonplorem_headless_react=this.webpackJsonplorem_headless_react||[]).push([[0],{40:function(e,t,n){},64:function(e,t,n){"use strict";n.r(t);var r=n(0),c=n(15),a=n.n(c),l=n(11),s=n(9),o=n(10),i=n(13),d=Object(i.b)({name:"react",initialState:{url:"/api/"},reducers:{}}).reducer,u=Object(i.b)({name:"model",initialState:{error:null,loading:!1,model:{}},reducers:{getModelStart:function(e){e.loading=!0},getModelSuccess:function(e,t){e.model=t.payload,e.loading=!1},getModelFailed:function(e,t){e.error=t.payload,e.loading=!1}}}),j=u.actions,b=j.getModelStart,x=j.getModelSuccess,m=j.getModelFailed,h=u.reducer,f=function(e){return e.api.url},p=function(e){return e.model},O=function(e){var t;return null===(t=e.model.model)||void 0===t?void 0:t.content},v=Object(o.b)({api:d,model:h}),g=(n(40),n(3)),w=n(14),y=n.n(w),N=n(19),_=n(31),M=n.n(_),S=function(){var e=Object(N.a)(y.a.mark((function e(t){var n,r,c,a,l=arguments;return y.a.wrap((function(e){for(;;)switch(e.prev=e.next){case 0:return n=l.length>1&&void 0!==l[1]?l[1]:"index",r=l.length>2?l[2]:void 0,c="".concat(t).concat(n,".json"),r&&(c+=r),e.next=6,M.a.get(c);case 6:return a=e.sent,e.abrupt("return",a.data);case 8:case"end":return e.stop()}}),e)})));return function(t){return e.apply(this,arguments)}}(),C=n(1);var F=function(){return Object(C.jsx)("div",{className:"fixed top-1/2 left-1/2 -mt-24 -ml-56 z-50",children:Object(C.jsxs)("svg",{className:"animate-spin h-24 w-24 text-blue",xmlns:"http://www.w3.org/2000/svg",fill:"none",viewBox:"0 0 24 24",children:[Object(C.jsx)("circle",{className:"opacity-25",cx:"12",cy:"12",r:"10",stroke:"currentColor",strokeWidth:"4"}),Object(C.jsx)("path",{className:"opacity-75",fill:"currentColor",d:"M4 12a8 8 0 018-8V0C5.373 0 0 5.373 0 12h4zm2 5.291A7.962 7.962 0 014 12H0c0 3.042 1.135 5.824 3 7.938l3-2.647z"})]})})},k=n(17);function T(e){var t=e.heading,n=e.message;return Object(C.jsxs)("div",{role:"alert",children:[Object(C.jsx)("div",{className:"bg-red-500 text-white font-bold rounded-t px-4 py-2",children:t}),Object(C.jsx)("div",{className:"border border-t-0 border-red-400 rounded-b bg-red-100 px-4 py-3 text-red-700",children:Object(C.jsx)("p",{children:n})})]})}function z(e){var t=e.heading,n=e.message;return Object(C.jsxs)("div",{role:"alert",children:[Object(C.jsx)("div",{className:"bg-blue-500 text-white font-bold rounded-t px-4 py-2",children:t}),Object(C.jsx)("div",{className:"border border-t-0 border-blue-400 rounded-b bg-blue-100 px-4 py-3 text-blue-700",children:Object(C.jsx)("p",{children:n})})]})}var L=function(e){var t=e.heading,n=e.message,r=e.type;return Object(C.jsx)("div",{className:"fixed top-1/2 left-1/2 -mt-24 -ml-96 z-50",children:function(){switch(r){case"error":return Object(C.jsx)(T,{heading:t,message:n});default:return Object(C.jsx)(z,{heading:t,message:n})}}()})};function P(e){var t=e.name,n=e.url;return Object(C.jsx)("div",{className:"flex-1 flex",children:Object(C.jsxs)("a",{href:n,title:"tiff.se: a simple blog",className:"flex text-gray-600 font-medium transition-colors duration-200 hover:text-gray-900",children:[Object(C.jsx)("span",{className:"align-bottom material-icons-outlined mr-2 pt-1 text-3xl",children:"api"}),t]})})}var B=function(e){var t=e.children;return Object(C.jsxs)("div",{children:[Object(C.jsx)("header",{className:"pt-2 border-b-2 border-greyLight",children:Object(C.jsx)("div",{children:Object(C.jsx)("div",{className:"flex flex-col  px-2 my-2 space-y-6 mx-auto max-w-5xl",children:Object(C.jsx)("div",{className:"w-full flex justify-between",children:Object(C.jsx)(P,{})})})})}),Object(C.jsx)("main",{className:"flex flex-col px-2 my-2 space-y-6 mx-auto max-w-5xl",children:t})]})};var I=function(e){var t=e.heading,n=e.preamble,r=e.articles;return Object(C.jsxs)(B,{children:[Object(C.jsx)("h1",{className:"text-5xl mb-4 font-semibold",children:t}),Object(C.jsx)("p",{className:"text-2xl font-medium mb-10",children:n}),Object(C.jsx)("div",{children:r.map((function(e){return Object(C.jsx)("article",{className:"shadow-sm flex flex-col my-4 space-y-3 border-l-8 border-blue-600",children:Object(C.jsx)("div",{className:"border pl-4 pr-4 py-4",children:Object(C.jsxs)(l.b,{className:"no-underline text-2xl font-bold sm:text-4xl",to:e.url,children:[Object(C.jsx)("h1",{className:"mb-4",children:e.heading}),Object(C.jsx)("p",{className:"w-full text-lg font-medium leading-normal !no-underline",children:n})]})})})}))})]})};function A(e){var t=e.breadcrumbs;return Object(C.jsx)("nav",{children:Object(C.jsx)("ol",{className:"list-reset flex",children:null===t||void 0===t?void 0:t.map((function(e,t){return Object(C.jsx)("li",{className:"breadcrumbs__item parent mr-1",children:Object(C.jsx)(l.b,{to:e.url,children:e.text})},t)}))})})}var E=function(){var e=Object(s.c)((function(e){return e.model.model.breadcrumbs}));return Object(C.jsx)(A,Object(k.a)({},e))};var H=function(e){var t=e.heading,n=e.preamble,r=e.text;return Object(C.jsxs)(B,{children:[Object(C.jsx)(E,{}),Object(C.jsx)("h1",{className:"text-5xl mb-4 font-semibold",children:t}),Object(C.jsx)("p",{className:"text-2xl font-medium mb-10",children:n}),Object(C.jsx)("div",{className:"article",dangerouslySetInnerHTML:{__html:r}})]})};var J=function(e){var t=e.content;switch(t.modelType){case"StartPage":return Object(C.jsx)(I,Object(k.a)({},t));case"ArticlePage":return Object(C.jsx)(H,Object(k.a)({},t));default:return Object(C.jsx)(L,{type:"error",heading:"Could not find content component",message:'Could not find content component with modelType "'.concat(t.modelType,'", add it in the ContentFactory component')})}};function R(e){var t=e.getModel,n=Object(s.b)(),c=Object(s.c)(f),a=Object(s.c)(p),l=a.loading,o=a.error,i=Object(s.c)(O),d=Object(g.f)().route;return Object(r.useEffect)((function(){function e(){return(e=Object(N.a)(y.a.mark((function e(){var r;return y.a.wrap((function(e){for(;;)switch(e.prev=e.next){case 0:return n(b()),e.prev=1,e.next=4,t(c,d);case 4:r=e.sent,n(x(r)),e.next=11;break;case 8:e.prev=8,e.t0=e.catch(1),n(m(e.t0.message));case 11:case"end":return e.stop()}}),e,null,[[1,8]])})))).apply(this,arguments)}"undefined"!==typeof window&&window.__model&&(delete window.__model,1)||function(){e.apply(this,arguments)}()}),[c,d,t,n]),Object(C.jsxs)(C.Fragment,{children:[l&&Object(C.jsx)(F,{}),o&&Object(C.jsx)(L,{type:"error",heading:"Failed load data",message:o}),i&&!o&&Object(C.jsx)(J,{content:i})]})}var D=function(){return Object(C.jsx)(R,{getModel:S})};n(63);var V,W=function(e){var t=e.Router,n=function(e){var t="";return(null===e||void 0===e?void 0:e.url)?t=null===e||void 0===e?void 0:e.url:"undefined"!==typeof window&&(t=window.location.pathname),t}(Object(s.c)(O));return Object(C.jsx)(t,{location:n,children:Object(C.jsx)(g.c,{children:Object(C.jsx)(g.a,{path:"/:route*",children:Object(C.jsx)(D,{})},"/:route*")})})},q=function(e){e&&e instanceof Function&&n.e(3).then(n.bind(null,65)).then((function(t){var n=t.getCLS,r=t.getFID,c=t.getFCP,a=t.getLCP,l=t.getTTFB;n(e),r(e),c(e),a(e),l(e)}))},G=a.a.render,K={};window.__model&&(K={api:{url:"/"},model:{model:window.__model}},G=a.a.hydrate),G(Object(C.jsx)(s.a,{store:(V=K,Object(i.a)({reducer:v,preloadedState:V})),children:Object(C.jsx)(W,{Router:l.a})}),document.getElementById("root")),q()}},[[64,1,2]]]);
//# sourceMappingURL=main.b35bf358.chunk.js.map