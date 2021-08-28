using EPiServer.Core;
using EPiServer.DataAnnotations;
using EPiServer.Web;
using System.ComponentModel.DataAnnotations;

namespace lorem_headless.Models.Pages
{
    [ContentType(GUID = "62E61CA4-168B-4823-99F7-5443313D2073", DisplayName = "Start")]
    public class StartPage
        : PageData
    {
        [Display(Order = 1)]
        public virtual string Heading { get; set; }

        [Display(Order = 2), UIHint(UIHint.Textarea)]
        public virtual string Preamble {  get; set; }

        [Display(Order = 3)]
        public virtual ContentArea ContentArea { get; set; }
    }
}