using System.Collections.Generic;

namespace lorem_headless.Features.Breadcrumbs.Models
{
    public class BreadcrumbsModel
    {
        public BreadcrumbsModel()
        {
            Breadcrumbs = new List<Breadcrumb>();
        }

        public List<Breadcrumb> Breadcrumbs { get; set; }

        public void Add(Breadcrumb breadcrumb) 
        {
            if(breadcrumb != null)
            {
                Breadcrumbs.Insert(0, breadcrumb);
            }
        }
    }
}