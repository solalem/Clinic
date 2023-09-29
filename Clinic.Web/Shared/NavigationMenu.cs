using Blazorise;
using System;
using System.Collections.Generic;

namespace Clinic.Web.Shared
{
    public class NavigationMenu
    {
        public List<NavigationMenuItem> Items { get; set; } = new List<NavigationMenuItem>();
    }

    public class NavigationMenuItem
    {
        public string Title { get; set; }
        public string Handler { get; set; }
        public string Page { get; set; }
        public string Module { get; set; }
        public bool IsAction { get; set; }
        public string Link { get; set; }
        public IconName IconId { get; set; }
        public List<NavigationMenuItem> SubMenus { get; } = new List<NavigationMenuItem>();
    }
}
