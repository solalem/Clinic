using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Linq;

namespace Clinic.Web.Components
{
    public class BlazorComponent : ComponentBase
    {
        private readonly RefreshBroadcast _refresh = RefreshBroadcast.Instance;

        protected override void OnInitialized()
        {
            _refresh.RefreshRequested += DoRefresh;
            base.OnInitialized();
        }

        public void CallRequestRefresh()
        {
            _refresh.CallRequestRefresh();
        }

        private void DoRefresh()
        {
            StateHasChanged();
        }

    }

    public class BlazorPage : BlazorComponent
    {
        [Inject]
        protected NavigationManager _navManager { get; set; }
        [Inject]
        protected PageHistoryState _pageState { get; set; }
        public BlazorPage(NavigationManager navManager, PageHistoryState pageState)
        {
            _navManager = navManager;
            _pageState = pageState;
        }
        public BlazorPage()
        {
        }

        protected override void OnInitialized()
        {
            base.OnInitialized();
            _pageState.AddPage(_navManager.Uri);
        }
    }

    public class PageHistoryState
    {
        private List<string> previousPages;

        public PageHistoryState()
        {
            previousPages = new List<string>();
        }
        public void AddPage(string pageName)
        {
            if (previousPages.Count > 0 && previousPages[previousPages.Count - 1] == pageName)
                return;

            // Go upto 20 pages
            if (previousPages.Count > 20)
                previousPages.Clear();
            previousPages.Add(pageName);
        }

        public string PreviousPage()
        {
            if (previousPages.Count > 1)
            {
                // You add a page on initialization, so you need to return the 2nd from the last
                return previousPages.ElementAt(previousPages.Count - 2);
            }

            // Can't go back because you didn't navigate enough
            return previousPages.FirstOrDefault();
        }

        public bool CanGoBack()
        {
            return previousPages.Count > 1;
        }
    }
}
