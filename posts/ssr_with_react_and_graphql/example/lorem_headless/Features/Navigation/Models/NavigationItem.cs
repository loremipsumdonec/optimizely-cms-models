using EPiServer.Core;
using System;
using System.Collections.Generic;

namespace lorem_headless.Features.Navigation.Models
{
    public class NavigationItem
    {
        private readonly Lazy<IEnumerable<NavigationItem>> _loadItems;
        private List<NavigationItem> _items = new List<NavigationItem>();

        public NavigationItem()
        {
        }

        public NavigationItem(Func<IEnumerable<NavigationItem>> loadItems)
        {
            _loadItems = new Lazy<IEnumerable<NavigationItem>>(loadItems);
        }

        public string Url { get; set; }

        public string Text { get; set; }

        public List<NavigationItem> Items 
        { 
            get 
            {
                if(_loadItems != null 
                    && !_loadItems.IsValueCreated)
                {
                    foreach(var item in _loadItems.Value) 
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