using EPiServer.Data;
using EPiServer.Framework;
using EPiServer.Framework.Initialization;
using lorem_headless.Features.WebApi.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace lorem_headless.Features.WebApi.Initialization
{
    [InitializableModule]
    [ModuleDependency(typeof(DataInitialization))]
    public class WebApiDependencyResolver
        : IInitializableModule
    {
        public void Initialize(InitializationEngine context)
        {
            GlobalConfiguration.Configuration.DependencyResolver = new StructureMapWebApiDependencyResolver(
                context.Locate.Advanced.GetInstance<StructureMap.IContainer>()
            );
        }

        public void Uninitialize(InitializationEngine context)
        {
        }
    }
}