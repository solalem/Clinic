using Clinic.ViewModels;
using Clinic.ViewModels.Appointments.Patients;
using Clinic.Web.Shared.Components;
using Microsoft.AspNetCore.Components;
using Clinic.Web.Models;

namespace Clinic.Web.Areas.Appointments.Patients.Pages
{
    public partial class Index : BlazorPage
    {
        private bool showPreview = false;
        private PatientList patients = PatientList.Empty();
    
        [Inject]
        public IAppointmentsServices AppointmentsServices { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }

        public PatientList Patients { get => patients; }

        public DialogBoxComponent DialogBox;
        
        public Details Details { get; set; } //= new Details();

        [Parameter]
        [SupplyParameterFromQuery]
        public Guid HighlightId { get; set; }

        protected override async Task OnParametersSetAsync()
        {
            await ReloadPatients(patients.PaginationInfo);
            if (HighlightId != Guid.Empty && Details != null)
            {
                showPreview = true;
                await Details.Open(HighlightId);
            }
        }

        public void CreateClick()
        {
            NavigationManager.NavigateTo($"Appointments/Patients/new");
        }

        public void ToggleShowPreview()
        {
            showPreview = !showPreview;
            StateHasChanged();
        }

        public async Task DetailClick(PatientSummary item)
        {
            if (item == null)
                return;

            showPreview = true;

            await Details.Open(item.Id);
        }

        public async Task DeleteClick(PatientSummary item)
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
                await AppointmentsServices.PatientService.DeleteAsync(context.TagId);
                await ReloadPatients(Patients.PaginationInfo);
            }
        }

        public async Task Search(Microsoft.AspNetCore.Components.Web.KeyboardEventArgs e)
        {
            if (e.Code == "Enter" || e.Code == "NumpadEnter")
            {
                await ReloadPatients(Patients.PaginationInfo);
            }
        }
        public async Task ReloadPatients(PaginationInfo pagination)
        {
            var response = await AppointmentsServices.PatientService.ListAsync(new GetPatientsRequest(pagination));
            if (response != null && response.Succeed)
            {
                patients = response.Data;
                showPreview = false;
            }
            CallRequestRefresh();
        }
    }
}
