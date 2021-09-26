using StructureMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Dependencies;

namespace lorem_headless.Features.WebApi.Services
{
    public class StructureMapWebApiDependencyResolver
        : IDependencyResolver
    {
        private readonly IContainer _container;
        private bool _disposed;

        public StructureMapWebApiDependencyResolver(IContainer container)
        {
            _container = container;
        }

        ~StructureMapWebApiDependencyResolver()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            if (disposing)
            {
                _container.Dispose();
            }

            _disposed = true;
        }

        public IDependencyScope BeginScope()
        {
            var container = _container.GetNestedContainer();
            return new StructureMapScope(container);
        }

        public object GetService(Type serviceType)
        {
            if (serviceType.IsInterface || serviceType.IsAbstract)
            {
                return GetInterfaceService(serviceType);
            }

            return GetConcreteService(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return _container.GetAllInstances(serviceType).Cast<object>();
        }

        private object GetConcreteService(Type serviceType)
        {
            try
            {
                return _container.GetInstance(serviceType);
            }
            catch (StructureMapException)
            {
                return null;
            }
        }

        private object GetInterfaceService(Type serviceType)
        {
            return _container.TryGetInstance(serviceType);
        }
    }
}