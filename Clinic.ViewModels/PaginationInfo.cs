using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Clinic.ViewModels
{
    public class PaginationInfo
    {
        public int TotalItems { get; set; }
        public int PageSize { get; set; } = 20;
        public int Page { get; set; } = 1;
        public int Index { get => PageSize * (Page - 1); }
        public int TotalPages { get => (int)Math.Ceiling(1.0 * TotalItems / PageSize); }
        //public string Previous { get; set; }
        public string SearchString { get; set; } = "";
        public bool IsSearch { get; set; }
        //public string Next { get; set; }
        //public string AspArea { get; set; }
        //public string AspPageName { get; set; }
        //public string AspParameters { get; set; }
        //public string ParentView { get; set; }
        public bool Enabled { get; set; }
        public bool HasPrevious => Page > 1;
        public bool HasNext => Page < TotalPages;

        public PaginationInfo()
        {

        }

        public PaginationInfo(string parentView, string aspArea, string aspPageName, string aspParameters)
        {
            //ParentView = parentView;
            //AspArea = aspArea;
            //AspPageName = aspPageName;
            //AspParameters = aspParameters;
            Enabled = true;
        }

        public string GetUrlSuffixes()
        {
            return
                "page=" + Page +
                "&pageSize=" + PageSize +
                "&searchString=" + SearchString; // + (string.IsNullOrEmpty(AspParameters) ? "" : "&" + AspParameters);
        }
    }

    public class PagingLink
    {
        public string Text { get; set; }
        public int Page { get; set; }
        public bool Enabled { get; set; }
        public bool Active { get; set; }

        public PagingLink(int page, bool enabled, string text)
        {
            Page = page;
            Enabled = enabled;
            Text = text;
        }
    }
}
