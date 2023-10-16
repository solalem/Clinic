using Clinic.Web.Shared;
using Clinic.ViewModels;
using Clinic.ViewModels.Appointments;
using Clinic.Core.Appointments.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using Blazorise;
using System;
using Clinic.Web.Models;
using Clinic.Web.Areas.Appointments;
using Clinic.ViewModels.Appointments.Visits;
using Clinic.ViewModels.Appointments.Patients;

namespace Clinic.Core.Appointments.Pages.Visits
{
    public partial class Create: BlazorPage
    {
        [Inject]
        public IAppointmentsServices AppointmentsServices { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }

        private CreateVisit Item = new CreateVisit();

        [Parameter]
        [SupplyParameterFromQuery]
        public Guid? RoutedPatientId { get; set; }
        public PatientList PatientOptions { get; set; } = PatientList.Empty();
        private async Task LoadPatientOptions(PaginationInfo paginationInfo)
        {
            if (paginationInfo.IsSearch && string.IsNullOrEmpty(paginationInfo.SearchString))
                return;
            Logger.LogInformation("Now loading PatientOptions...");

            PatientOptions = await AppointmentsServices.PatientService.ListAsync(new GetPatients { PaginationInfo = PatientOptions.PaginationInfo });
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
                PatientOptions = await AppointmentsServices.PatientService.ListAsync(new GetPatients { PaginationInfo = PatientOptions.PaginationInfo });
                CallRequestRefresh();
            }

            await base.OnAfterRenderAsync(firstRender);
        }

        private async Task CreateClick()
        {
            try
            {
                var result = await AppointmentsServices.VisitService.CreateAsync(Item);

                NavigationManager.NavigateTo($"Appointments/Visits/Index?HighlightId={result.Id}");
            }
            catch (System.Exception)
            {
                throw;
            }
        }
    }
}
