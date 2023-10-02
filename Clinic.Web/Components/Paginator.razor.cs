using Clinic.ViewModels;
using Clinic.Web.Components;
using Microsoft.AspNetCore.Components;

namespace Clinic.Web.Shared.Components
{
    public partial class Paginator : BlazorComponent
    {
        [Parameter]
        public String Title { get; set; } = "Title";
        [Parameter]
        public PaginationInfo PaginationInfo { get; set; }
        [Parameter]
        public int Spread { get; set; }
        [Parameter]
        public RenderFragment ChildContent { get; set; }
        [Parameter]
        public EventCallback<PaginationInfo> PageSelected { get; set; }

        private List<PagingLink> _links;

        protected override void OnParametersSet()
        {
            CreatePaginationLinks();
        }

        private void CreatePaginationLinks()
        {
            _links = new List<PagingLink>();

            _links.Add(new PagingLink(PaginationInfo.Index - 1, PaginationInfo.HasPrevious, "«"));

            for (int i = 1; i <= PaginationInfo.TotalPages; i++)
            {
                if (i >= PaginationInfo.Index - Spread && i <= PaginationInfo.Index + Spread)
                {
                    _links.Add(new PagingLink(i, true, i.ToString()) { Active = PaginationInfo.Index == i });
                }
            }

            _links.Add(new PagingLink(PaginationInfo.Index + 1, PaginationInfo.HasNext, "»"));
        }

        private async Task OnPageSelected(PagingLink link)
        {
            if (link.Page == PaginationInfo.Index || !link.Enabled)
                return;

            PaginationInfo.Page = link.Page;
            await PageSelected.InvokeAsync(PaginationInfo);
        }
    }
}
