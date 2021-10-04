using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using EPiServer.Shell.ObjectEditing;
using EPiServer.Web;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace lorem_headless.Models.Pages
{
    [ContentType(GUID = "62E61CA4-168B-4823-99F7-5443313D2073", DisplayName = "Start")]
    public class StartPage
        : SitePage
    {
        [Display(Order = 2), UIHint(UIHint.Textarea)]
        public virtual string Preamble {  get; set; }

        [Display(Order = 3)]
        public virtual ContentArea ContentArea { get; set; }

        [SelectOne(SelectionFactoryType = typeof(RendererSelectionFactory))]
        [Display(GroupName = SystemTabNames.PageHeader, Order = 110, Description = "Change default render engine")]
        public virtual string Renderer {  get; set; }
    }

    public class RendererSelectionFactory
        : ISelectionFactory
    {
        public IEnumerable<ISelectItem> GetSelections(ExtendedMetadata metadata)
        {
            return new List<ISelectItem>() { 
                new SelectItem() { Text = "Headless", Value = "Headless" },
                new SelectItem() { Text = "SpaceX", Value = "SpaceX" },
            };
        }
    }
}