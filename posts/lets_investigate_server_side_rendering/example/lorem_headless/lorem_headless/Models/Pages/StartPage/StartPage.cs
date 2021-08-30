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
        : PageData
    {
        [Display(Order = 1)]
        public virtual string Heading { get; set; }

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
                new SelectItem() { Text = "Razor", Value = "razor" },
                new SelectItem() { Text = "React, create react app", Value = "create-react-app" },
                new SelectItem() { Text = "React, create react app with html", Value = "create-react-app-with-html" },
            };
        }
    }
}