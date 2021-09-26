using EPiServer.Core;
using EPiServer.DataAnnotations;
using lorem_headless.Features.Navigation.Models;
using System.ComponentModel.DataAnnotations;

namespace lorem_headless.Models.Pages
{
    [ContentType(
        GUID = "A5FA4A31-4BA2-4B96-B0D1-30D4D51CEA66", 
        DisplayName = "Settings")]
    public class SettingsPage
        : PageData
    {
        [Display(Order = 2, Name = "Navigation")]
        public virtual NavigationSettingsBlock NavigationSettings { get; set; }

    }
}