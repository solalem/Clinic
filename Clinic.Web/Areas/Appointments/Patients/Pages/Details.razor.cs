using Clinic.ViewModels.Appointments.Patients;
using Clinic.Web.Areas.Appointments.Patients.Components;
using Clinic.Web.Shared.Components;
using Microsoft.AspNetCore.Components;
using Clinic.Web.Models;

namespace Clinic.Web.Areas.Appointments.Patients.Pages
{
    public partial class Details : BlazorPage
    {
        private PatientDetail Item = new PatientDetail();
        private DialogBoxComponent DialogBox;
        private PatientInfo info;

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
                await AppointmentsServices.PatientService.DeleteAsync(context.TagId);
                if (OnDeleteClick.HasDelegate)
                    await OnDeleteClick.InvokeAsync(null);
          
                NavigationManager.NavigateTo($"Appointments/Patients/Index?HighlightId={Guid.Empty}");
            }
        }

        protected override async Task OnParametersSetAsync()
        {
            await Open(Id);
        }

        public async Task Open(Guid id)
        {
            if (id == Guid.Empty) return;
            Id = id;

            Logger.LogInformation("Now loading Patient... {Id}", Id);

            var response = await AppointmentsServices.PatientService.GetAsync(new GetPatientRequest(Id));
            if (response != null && response.Succeed)
                await info.Open(Item);

            StateHasChanged();
        }

        string selectedTab = "More";
        private Task OnSelectedTabChanged(string name)
        {
            selectedTab = name;
            return Task.CompletedTask;
        }
    }
}
