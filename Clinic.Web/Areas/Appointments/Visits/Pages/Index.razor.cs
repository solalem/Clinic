using Clinic.ViewModels;
using Clinic.ViewModels.Appointments.Visits;
using Clinic.Web.Models;
using Clinic.Web.Shared.Components;
using Microsoft.AspNetCore.Components;

namespace Clinic.Web.Areas.Appointments.Visits.Pages
{
    public partial class Index : BlazorPage
    {
        private bool showPreview = false;
        private VisitList visits = VisitList.Empty();
    
        [Inject]
        public IAppointmentsServices AppointmentsServices { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }

        public VisitList Visits { get => visits; }

        public DialogBoxComponent DialogBox;
        
        public Details Details { get; set; }

        [Parameter]
        [SupplyParameterFromQuery]
        public Guid HighlightId { get; set; }

        protected override async Task OnParametersSetAsync()
        {
            await ReloadVisits(visits.PaginationInfo);
            if (HighlightId != Guid.Empty && Details != null)
            {
                showPreview = true;
                await Details.Open(HighlightId);
            }
        }

        public void CreateClick()
        {
            NavigationManager.NavigateTo($"Appointments/Visits/new");
        }

        public void ToggleShowPreview()
        {
            showPreview = !showPreview;
            StateHasChanged();
        }

        public async Task DetailClick(VisitSummary item)
        {
            if (item == null)
                return;

            showPreview = true;
            await Details.Open(item.Id);
        }

        public async Task DeleteClick(VisitSummary item)
        {
            await DialogBox.Open(
                new DialogContext(
                    "The selected item will be deleted. Are you sure to continue?", 
                    "Deleting Item", 
                    item.Id, 
                    "delete")
                );
        }

        public async Task DialogResultReceive(DialogContext context)
        {
            if (context.Type == "delete" && context.Result == DialogResult.Accept)
            {
                await AppointmentsServices.VisitService.DeleteAsync(new(context.TagId));
                await ReloadVisits(Visits.PaginationInfo);
            }
        }

        public async Task Search(Microsoft.AspNetCore.Components.Web.KeyboardEventArgs e)
        {
            if (e.Code == "Enter" || e.Code == "NumpadEnter")
            {
                await ReloadVisits(Visits.PaginationInfo);
            }
        }
        public async Task ReloadVisits(PaginationInfo pagination)
        {
            var response = await AppointmentsServices.VisitService.ListAsync(new(pagination));
            if (response != null && response.Succeed)
                visits = response.Data;
            showPreview = false;
            CallRequestRefresh();
        }
    }
}
