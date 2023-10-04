using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Linq;

namespace Clinic.Web.Models
{
    public class BlazorComponent : ComponentBase
    {
        private readonly RefreshBroadcast _refresh = RefreshBroadcast.Instance;

        protected override void OnInitialized()
        {
            _refresh.RefreshRequested += DoRefresh;
            base.OnInitialized();
        }

        public void Dispose()
        {
            _refresh.RefreshRequested -= DoRefresh;
        }

        public void CallRequestRefresh()
        {
            _refresh.CallRequestRefresh();
        }

        private async void DoRefresh()
        {
            await InvokeAsync(() => StateHasChanged());
        }

    }
}
