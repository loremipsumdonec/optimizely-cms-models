using System;
using System.Collections.Generic;

namespace lorem_headless.Features.Navigation.Models
{
    public class NavigationModel 
    {
        private readonly Lazy<IEnumerable<NavigationItem>> _loadItems;
        private List<NavigationItem> _items = new List<NavigationItem>();

        public NavigationModel()
        {
        }

        public NavigationModel(Func<IEnumerable<NavigationItem>> loadItems)
        {
            _loadItems = new Lazy<IEnumerable<NavigationItem>>(loadItems);
        }

        public string AccessibilityDescription { get; set; }

        public string OpenNavigationPaneLabel { get; set; }

        public string CloseNavigationPaneLabel { get; set; }

        public string OpenNavigationItemLabel { get; set; }

        public string CloseNavigationItemLabel { get; set; }

        public List<NavigationItem> Items
        {
            get
            {
                if (_loadItems != null
                    && !_loadItems.IsValueCreated)
                {
                    foreach (var item in _loadItems.Value)
                    {
                        Add(item);
                    }
                }

                return _items;
            }
            set
            {
                _items = value;
            }
        }

        public void Add(NavigationItem item)
        {
            _items.Add(item);
        }
    }
}