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

namespace Solo.Appointments.Components
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
