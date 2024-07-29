using Clinic.ViewModels;
using Clinic.ViewModels.Appointments.Patients;
using Clinic.ViewModels.Appointments.Visits;
using Clinic.Web.Areas.Appointments;
using Clinic.Web.Models;
using Clinic.Web.Shared.Components;
using Microsoft.AspNetCore.Components;

namespace Clinic.Web.Areas.Appointments.Visits.Components
{
    public partial class VisitInfo : BlazorComponent
    {
        private DialogBoxComponent DialogBox;
        
        [Inject]
        public NavigationManager NavigationManager { get; set; }
        [Inject]
        public IAppointmentsServices AppointmentsServices { get; set; }

        [Parameter]
        public VisitSummary Item { get; set; } = new VisitSummary();

        private async Task UpdateClick()
        {
            await AppointmentsServices.VisitService.UpdateAsync(new UpdateVisit
            {
                Id = Item.Id,
                PatientId = Item.PatientId,
                Physician = Item.Physician,
                Description = Item.Description,
                Procedures = Item.Procedures,
            });
        }

        public async Task Open(VisitSummary item)
        {
            if (item == null) return;

            Item = item;
            await LoadPatientOptions(new PaginationInfo());

            StateHasChanged();
        }

        PatientList PatientOptions = PatientList.Empty();
        private async Task LoadPatientOptions(PaginationInfo paginationInfo)
        {
            if (paginationInfo.IsSearch && string.IsNullOrEmpty(paginationInfo.SearchString))
                return;
            Logger.LogInformation("Now loading PatientOptions...");

            PatientOptions = await AppointmentsServices.PatientService.ListAsync(new GetPatients { PaginationInfo = paginationInfo });
        }
        private void SetPatientId(PatientSummary item)
        {
            Item.PatientId =item.Id;
            StateHasChanged();
        }


    }
}
