using Clinic.ViewModels.Appointments.Patients;
using Clinic.Web.Models;
using Microsoft.AspNetCore.Components;

namespace Clinic.Web.Areas.Appointments.Patients.Pages
{
    public partial class Create: BlazorPage
    {
        [Inject]
        public IAppointmentsServices AppointmentsServices { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }

        private CreatePatientRequest Item = new CreatePatientRequest();
        public bool UseDOB { get; set; }


        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                CallRequestRefresh();
            }

            await base.OnAfterRenderAsync(firstRender);
        }

        private void AgeChanged(int age)
        {
            Item.Age = age;
            Item.DateOfBirth = DateTime.Now.AddYears(-age);
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
