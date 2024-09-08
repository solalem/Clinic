using Clinic.ViewModels;
using Microsoft.AspNetCore.Components;
using Clinic.Web.Models;
using Clinic.ViewModels.Appointments.Visits;
using Clinic.ViewModels.Appointments.Patients;

namespace Clinic.Web.Areas.Appointments.Visits.Pages
{
    public partial class Create: BlazorPage
    {
        [Inject]
        public IAppointmentsServices AppointmentsServices { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }

        private CreateVisitRequest Item = new CreateVisitRequest();

        [Parameter]
        [SupplyParameterFromQuery]
        public Guid? RoutedPatientId { get; set; }
        public PatientList PatientOptions { get; set; } = PatientList.Empty();
        private async Task LoadPatientOptions(PaginationInfo paginationInfo)
        {
            if (paginationInfo.IsSearch && string.IsNullOrEmpty(paginationInfo.SearchString))
                return;
            Logger.LogInformation("Now loading PatientOptions...");

            var response = await AppointmentsServices.PatientService.ListAsync(new GetPatientsRequest(PatientOptions.PaginationInfo));
            if (response != null && response.Succeed)
                PatientOptions = response.Data;
            
            StateHasChanged();
        }
        private void SetPatientId(PatientSummary item)
        {
            Item.PatientId =item.Id;
            StateHasChanged();
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                if(RoutedPatientId.HasValue)
                    Item.PatientId = RoutedPatientId.Value;
                await LoadPatientOptions(PatientOptions.PaginationInfo);
                
                CallRequestRefresh();
            }

            await base.OnAfterRenderAsync(firstRender);
        }

        private async Task CreateClick()
        {
            try
            {
                var result = await AppointmentsServices.VisitService.CreateAsync(Item);

                NavigationManager.NavigateTo($"Appointments/Visits/Index?HighlightId={result.Data.Id}");
            }
            catch (System.Exception)
            {
                throw;
            }
        }
    }
}
