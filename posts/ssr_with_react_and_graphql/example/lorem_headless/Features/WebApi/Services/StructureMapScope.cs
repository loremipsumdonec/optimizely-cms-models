using StructureMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Dependencies;

namespace lorem_headless.Features.WebApi.Services
{
    public class StructureMapScope
        : IDependencyScope
    {
        private readonly IContainer _container;
        private bool _disposed;

        public StructureMapScope(IContainer container)
        {
            _container = container;
        }

        ~StructureMapScope()
        {
            Dispose(false);
        }

        public object GetService(Type serviceType)
        {
            if (serviceType == null) return null;
            if (serviceType.IsAbstract || serviceType.IsInterface) return _container.TryGetInstance(serviceType);

            return _container.GetInstance(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return _container.GetAllInstances(serviceType).Cast<object>();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            if (disposing)
            {
                _container.Dispose();
            }

            _disposed = true;
        }
    }
}