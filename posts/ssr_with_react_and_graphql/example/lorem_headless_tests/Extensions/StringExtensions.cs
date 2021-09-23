using AngleSharp;
using System.Linq;
using System.Threading.Tasks;

namespace lorem_headless_tests.Extensions
{
    public static class StringExtensions
    {
        public static async Task<string> GetTextAsync(this string html, string localName) 
        {
            var config = Configuration.Default;
            var document = await BrowsingContext.New(config).OpenAsync(r => r.Content(html));

            return document.All.FirstOrDefault(e => e.LocalName == localName).TextContent;
        }
    }
}
