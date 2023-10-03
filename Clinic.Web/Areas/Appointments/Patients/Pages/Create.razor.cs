using Clinic.ViewModels.Appointments.Patients;
using Clinic.Web.Components;
using Microsoft.AspNetCore.Components;

namespace Clinic.Web.Areas.Appointments.Pages.Patients
{
    public partial class Create: BlazorPage
    {
        [Inject]
        public IAppointmentsServices AppointmentsServices { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }

        private CreatePatient Item = new CreatePatient();


        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                CallRequestRefresh();
            }

            await base.OnAfterRenderAsync(firstRender);
        }

        private async Task CreateClick()
        {
            try
            {
                var result = await AppointmentsServices.PatientService.CreateAsync(Item);

                NavigationManager.NavigateTo($"Appointments/Patients/Index?HighlightId={result.Id}");
            }
            catch (System.Exception)
            {
                throw;
            }
        }
    }
}
