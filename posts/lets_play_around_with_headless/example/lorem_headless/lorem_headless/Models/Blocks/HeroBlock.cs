using EPiServer.Core;
using EPiServer.DataAnnotations;
using EPiServer.Web;
using System.ComponentModel.DataAnnotations;

namespace lorem_headless.Models.Blocks
{
    [ContentType(GUID = "C33FCC64-07AC-4097-8461-03CB43DB1464", DisplayName = "Hero")]
    public class HeroBlock
        : BlockData
    {
        [Display(Order = 1)]
        public virtual string Heading { get; set; }

        [Display(Order = 2), UIHint(UIHint.Textarea)]
        public virtual string Preamble { get; set; }
    }
}