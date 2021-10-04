
namespace lorem_headless.Models.Pages
{
    public class StartPageModel
        : SitePageModel
    {
        public StartPageModel()
        {
        }

        public StartPageModel(StartPage startPage) 
        {
            Heading = startPage.Heading;
            Preamble = startPage.Preamble;
        }

        public string Preamble { get; set; }
    
        public string ModelType { get; } = nameof(StartPage);
    }
}