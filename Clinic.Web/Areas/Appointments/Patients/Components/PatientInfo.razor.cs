using Clinic.Web.Shared.Components;
using Clinic.ViewModels.Appointments.Patients;
using Clinic.Web.Shared.Components;
using Microsoft.AspNetCore.Components;
using Clinic.Web.Models;

namespace Clinic.Web.Areas.Appointments.Patients.Components
{
    public partial class PatientInfo : BlazorComponent
    {
        private DialogBoxComponent DialogBox;
        
        [Inject]
        public NavigationManager NavigationManager { get; set; }
        [Inject]
        public IAppointmentsServices AppointmentsServices { get; set; }

        [Parameter]
        public PatientSummary Item { get; set; } = new PatientSummary();

        private async Task UpdateClick()
        {
            await AppointmentsServices.PatientService.UpdateAsync(new UpdatePatient
            {
                Id = Item.Id,
                CardNumber = Item.CardNumber,
                FullName = Item.FullName,
                Gender = Item.Gender,
                PhoneNumber = Item.PhoneNumber,
                DateOfBirth = Item.DateOfBirth,
                Email = Item.Email,
            });
        }

        public async Task Open(PatientSummary item)
        {
            if (item == null) return;

            Item = item;

            StateHasChanged();
        }


    }
}
