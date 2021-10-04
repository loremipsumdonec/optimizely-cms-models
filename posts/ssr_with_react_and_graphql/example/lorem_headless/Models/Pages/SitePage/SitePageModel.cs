using System;

namespace lorem_headless.Models.Pages
{
    public class SitePageModel
    {
        private Lazy<string> _loadUrl;

        public string Heading { get; set; }

        public string Url
        {
            get 
            {
                return _loadUrl?.Value ?? string.Empty;
            }
        }

        public void SetLoadUrl(Func<string> loadUrl) 
        {
            _loadUrl = new Lazy<string>(loadUrl);
        }
    }
}