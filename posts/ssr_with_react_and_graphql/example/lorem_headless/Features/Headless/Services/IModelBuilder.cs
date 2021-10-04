using EPiServer.Core;

namespace lorem_headless.Features.Headless.Services
{
    public interface IModelBuilder
    {
        void Build(object model, IContent source);
    }
}