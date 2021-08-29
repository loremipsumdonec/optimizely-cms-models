using EPiServer.Core;
using EPiServer.DataAnnotations;
using EPiServer.Web;
using System.ComponentModel.DataAnnotations;

namespace lorem_headless.Models.Pages
{
    [ContentType(GUID = "A8E658BE-71AC-477F-A672-2FC10C367C90", DisplayName = "Article")]
    public class ArticlePage
        : PageData
    {
        [Display(Order = 1)]
        public virtual string Heading { get; set; }

        [Display(Order = 2), UIHint(UIHint.Textarea)]
        public virtual string Preamble { get; set; }

        [Display(Order = 3)]
        public virtual XhtmlString Text { get; set; }
    }
}