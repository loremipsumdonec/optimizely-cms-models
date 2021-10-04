
namespace lorem_headless.Features.Headless.Models
{
    public class ContextModel
    {
        public ContextModel(ContextModelState state)
        {
            State = state;
        }

        public ContextModelState State { get; set; }

        public int PageId { get; set; }

        public string Url { get; set; }

        public string ModelType { get; set; }
    }
}