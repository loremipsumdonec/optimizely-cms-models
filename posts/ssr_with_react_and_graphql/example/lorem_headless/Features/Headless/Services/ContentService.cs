using EPiServer;
using EPiServer.Core;
using StructureMap;
using System;
using System.Collections.Generic;

namespace lorem_headless.Features.Headless.Services
{
    public class ContentService
    {
        private readonly IContentLoader _loader;
        private readonly IContainer _container;

        public ContentService(IContentLoader loader, IContainer container)
        {
            _loader = loader;
            _container = container;
        }

        public M GetContent<M>(int id, params Type[] builders)
        {
            IContent content = _loader.Get<IContent>(new ContentReference(id));
            var model = CreateModel<M>();

            foreach(var builder in CreateBuilders(builders)) 
            {
                builder.Build(model, content);
            }

            return (M)model;
        }

        public IEnumerable<M> GetChildren<T, M>(int parentId, params Type[] builders) 
            where T : IContentData
        {
            foreach(var content in _loader.GetChildren<T>(new ContentReference(parentId)))
            {
                var model = CreateModel<M>();

                foreach (var builder in CreateBuilders(builders))
                {
                    builder.Build(model, (IContent)content);
                }

                yield return (M)model;
            }
        }

        private object CreateModel<T>() 
        {
            return (T)Activator.CreateInstance(typeof(T));
        }

        private IEnumerable<IModelBuilder> CreateBuilders(params Type[] builders) 
        {
            foreach(var type in builders) 
            {
                yield return (IModelBuilder)_container.GetInstance(type);
            }
        }
    }
}