
using System.Collections.Generic;

namespace lorem_headless.Models.Pages
{
    public class StartPageModel
    {
        public StartPageModel(StartPage startPage) 
        {
            Heading = startPage.Heading;
            Preamble = startPage.Preamble;
            Articles = new List<object>();
        }

        public string Heading { get; set; }

        public string Preamble { get; set; }

        public string Url { get; set; }

        public List<object> Articles {  get; set; }
    
        public void Add(object article)
        {
            if(article != null)
            {
                Articles.Add(article);
            }
        }

        public string ModelType { get; } = nameof(StartPage);
    }
}