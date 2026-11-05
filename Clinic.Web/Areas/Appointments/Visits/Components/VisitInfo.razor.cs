using Clinic.ViewModels;
using Clinic.ViewModels.Appointments.Patients;
using Clinic.ViewModels.Appointments.Visits;
using Clinic.Web.Areas.Appointments;
using Clinic.Web.Models;
using Clinic.Web.Shared.Components;
using Microsoft.AspNetCore.Components;
using Clinic.Web.Security;

namespace Clinic.Web.Areas.Appointments.Visits.Components
{
    public partial class VisitInfo : BlazorComponent
    {
        private DialogBoxComponent DialogBox;
        
        [Inject]
        public NavigationManager NavigationManager { get; set; }
        [Inject]
        public IAppointmentsServices AppointmentsServices { get; set; }
        [Inject]
        public IClinicAccountService ClinicAccountService { get; set; }

        [Parameter]
        public VisitSummary Item { get; set; } = new VisitSummary();
        private IReadOnlyList<string> PhysicianOptions = Array.Empty<string>();

        private async Task UpdateClick()
        {
            await AppointmentsServices.VisitService.UpdateAsync(new UpdateVisitRequest
            {
                Id = Item.Id,
                Date = Item.Date,
                PatientId = Item.PatientId,
                Physician = Item.Physician,
                PresentIllness = Item.PresentIllness,
                Procedures = Item.ProcedureList,
                Investigations = Item.InvestigationList,
            });
        }

        public async Task Open(VisitSummary item)
        {
            if (item == null) return;

            Item = item;
            PhysicianOptions = await ClinicAccountService.GetPhysicianNamesAsync();
            await LoadPatientOptions(new PaginationInfo());

            StateHasChanged();
        }

        PatientList PatientOptions = PatientList.Empty();
        private async Task LoadPatientOptions(PaginationInfo paginationInfo)
        {
            if (paginationInfo.IsSearch && string.IsNullOrEmpty(paginationInfo.SearchString))
                return;
            Logger.LogInformation("Now loading PatientOptions...");

            var response = await AppointmentsServices.PatientService.ListAsync(new GetPatientsRequest(paginationInfo));
            if (response != null && response.Succeed)
                PatientOptions = response.Data;
        }
        private void SetPatientId(PatientSummary item)
        {
            Item.PatientId =item.Id;
            StateHasChanged();
        }


    }
}
