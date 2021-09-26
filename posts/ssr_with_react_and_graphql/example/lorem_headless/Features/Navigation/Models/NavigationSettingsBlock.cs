using EPiServer.Core;
using EPiServer.DataAnnotations;
using System.ComponentModel.DataAnnotations;

namespace lorem_headless.Features.Navigation.Models
{
    [ContentType(
        GUID = "CAA2752B-59EA-45C0-AD2E-53D9D3126BE9",
        DisplayName = "Navigation settings", 
        AvailableInEditMode = false)]
    public class NavigationSettingsBlock
        : BlockData
    {
        [Display(Order = 1, Name = "Description", Description = "A description used for accessibility")]
        public virtual string AccessibilityDescription { get; set; }

        [Display(Order = 2, Name = "Open pane label")]
        public virtual string OpenNavigationPaneLabel { get; set; }

        [Display(Order = 3, Name = "Close pane label")]
        public virtual string CloseNavigationPaneLabel { get; set; }

        [Display(Order = 4, Name = "Open item label")]
        public virtual string OpenNavigationItemLabel { get; set; }

        [Display(Order = 5, Name = "Close item label")]
        public virtual string CloseNavigationItemLabel { get; set; }
    }
}