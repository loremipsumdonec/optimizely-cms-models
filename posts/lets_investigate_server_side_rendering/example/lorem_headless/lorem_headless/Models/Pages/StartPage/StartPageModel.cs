
namespace lorem_headless.Models.Pages
{
    public class StartPageModel
    {
        public StartPageModel(StartPage startPage) 
        {
            Heading = startPage.Heading;
            Preamble = startPage.Preamble;
        }

        public string Heading { get; set; }

        public string Preamble { get; set; }
    }
}