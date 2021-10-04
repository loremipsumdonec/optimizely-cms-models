using lorem_headless.Features.Headless.Services;

namespace lorem_headless.Models.Pages
{
    public class StartPageModelBuilder
        : ModelBuilder<StartPageModel, StartPage>
    {
        public override void Build(StartPageModel model, StartPage source)
        {
            model.Heading = source.Heading;
            model.Preamble = source.Preamble;
        }
    }
}