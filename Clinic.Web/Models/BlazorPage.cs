using Microsoft.AspNetCore.Components;

namespace Clinic.Web.Models
{
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
}
