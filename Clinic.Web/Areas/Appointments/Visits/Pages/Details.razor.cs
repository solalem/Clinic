using Microsoft.AspNetCore.Components;
using Clinic.Web.Models;
using Clinic.ViewModels.Appointments.Visits;
using Clinic.Web.Shared.Components;
using Clinic.Web.Areas.Appointments.Visits.Components;
using Blazorise;

namespace Clinic.Web.Areas.Appointments.Visits.Pages
{
    public partial class Details : BlazorPage
    {
        private VisitDetail Item = new VisitDetail();
        private DialogBoxComponent DialogBox;
        private VisitInfo info;

        [Inject]
        public NavigationManager NavigationManager { get; set; }
        [Inject]
        public IAppointmentsServices AppointmentsServices { get; set; }

        [Parameter]
        public Guid Id { get; set; }
        
        [Parameter]
        public EventCallback<string> OnCloseClick { get; set; }
        private async Task CloseClick()
        {
            if(OnCloseClick.HasDelegate)
                await OnCloseClick.InvokeAsync(null);
            else
                NavigationManager.NavigateTo(_pageState.PreviousPage());
        }
        
        [Parameter]
        public EventCallback<string> OnDeleteClick { get; set; }
        public async Task DeleteClick(Guid id)
        {
            if (id == Guid.Empty)
                return;

            await DialogBox.Open(
                new DialogContext(
                    "The selected item will be deleted. Are you sure to continue?",
                    "Deleting Item",
                    id,
                    "delete")
                );
        }

        public async Task DialogResultReceive(DialogContext context)
        {
            if (context.Type == "delete" && context.Result == DialogResult.Accept)
            {
                await AppointmentsServices.VisitService.DeleteAsync(context.TagId);
                if (OnDeleteClick.HasDelegate)
                    await OnDeleteClick.InvokeAsync(null);
          
                NavigationManager.NavigateTo($"Appointments/Visits/Index?HighlightId={Guid.Empty}");
            }
        }

        private async Task UpdateClick()
        {
            await AppointmentsServices.VisitService.UpdateAsync(new UpdateVisit
            {
                Id = Item.Id,
                // Date = _item.CompanyName,
                Physician = Item.Physician,
                Description = Item.Description,
                PatientId = Item.PatientId,
                Procedures = Item.Procedures
            });
        }

        protected override async Task OnParametersSetAsync()
        {
            await Open(Id);
        }

        public async Task Open(Guid id)
        {
            if (id == Guid.Empty) return;
            Id = id;

            Logger.LogInformation("Now loading Visit... {Id}", Id);

            Item = await AppointmentsServices.VisitService.GetAsync(Id);
            await info.Open(Item);
            
            StateHasChanged();
        }


        string selectedTab = "More";
        private Task OnSelectedTabChanged(string name)
        {
            selectedTab = name;
            return Task.CompletedTask;
        }

        #region Procedures
        private Modal procedureModal;
        private ProcedureForm procedureForm;
        private ProcedureSummary selectedProcedure;
        private void AddProcedure()
        {
            selectedProcedure = new ProcedureSummary();
            Item.Procedures.Add(selectedProcedure);
            procedureModal.Show();
        }
        private void EditProcedure(ProcedureSummary procedure)
        {
            selectedProcedure = procedure;
            procedureModal.Show();
        }

        #endregion
    }
}
