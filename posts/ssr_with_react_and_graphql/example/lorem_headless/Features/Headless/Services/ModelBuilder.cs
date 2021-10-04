using EPiServer.Core;

namespace lorem_headless.Features.Headless.Services
{
    public abstract class ModelBuilder<M, T>
        : IModelBuilder
    {
        public void Build(object model, IContent source)
        {
            Build((M)model, (T)source);
        }

        public abstract void Build(M model, T source);
    }
}