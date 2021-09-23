using EPiServer.Core;
using System.ComponentModel.DataAnnotations;

namespace lorem_headless.Models.Pages
{
    public class SitePage
        : PageData
    {
        [Display(Order = 1)]
        public virtual string Heading { get; set; }
    }
}