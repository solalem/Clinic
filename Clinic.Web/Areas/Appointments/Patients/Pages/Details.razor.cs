using Solo.Shared.Helpers;
using Solo.Shared.Components;
using Solo.ViewModels.Appointments;
using Solo.ViewModels;
using Solo.Appointments.Services;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Blazorise;
using Solo.Shared.Models;
using Solo.Appointments.Components;

namespace Solo.Appointments.Pages.Patients
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

            Item = await AppointmentsServices.PatientService.GetAsync(Id);
            await info.Open(Item);
            
            await LoadRelatedAppointments(Item.Id, new PaginationInfo());
            await LoadRelatedAttendances(Item.Id, new PaginationInfo());
            StateHasChanged();
        }

        AppointmentList Appointments = AppointmentList.Empty();
        [Inject]
        Services.IAppointmentService AppointmentService { get; set; }
        private async Task LoadRelatedAppointments(Guid id, PaginationInfo pagination)
        {
            Logger.LogInformation("Loading Appointments...");
            Appointments = await AppointmentsServices.AppointmentService.ListAppointmentsByPatientIdAsync(id, pagination);
            StateHasChanged();
        }
        private void OpenAppointmentDetail(AppointmentSummary item)
        {
            NavigationManager.NavigateTo($"Appointments/Appointments/details/{item.Id}");
        }
        private void OpenAddAppointment()
        {
            NavigationManager.NavigateTo($"Appointments/Appointments/new?RoutedPatientId=" + Item.Id);
        }
        AttendanceList Attendances = AttendanceList.Empty();
        [Inject]
        Services.IAttendanceService AttendanceService { get; set; }
        private async Task LoadRelatedAttendances(Guid id, PaginationInfo pagination)
        {
            Logger.LogInformation("Loading Attendances...");
            Attendances = await AppointmentsServices.AttendanceService.ListAttendancesByPatientIdAsync(id, pagination);
            StateHasChanged();
        }
        private void OpenAttendanceDetail(AttendanceSummary item)
        {
            NavigationManager.NavigateTo($"Appointments/Attendances/details/{item.Id}");
        }
        private void OpenAddAttendance()
        {
            NavigationManager.NavigateTo($"Appointments/Attendances/new?RoutedPatientId=" + Item.Id);
        }

        string selectedTab = "More";
        private Task OnSelectedTabChanged(string name)
        {
            selectedTab = name;
            return Task.CompletedTask;
        }
    }
}
