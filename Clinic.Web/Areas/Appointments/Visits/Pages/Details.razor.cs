using Microsoft.AspNetCore.Components;
using Clinic.Web.Models;
using Clinic.ViewModels.Appointments.Visits;
using Clinic.Web.Shared.Components;
using Clinic.Web.Areas.Appointments.Visits.Components;
using Blazorise;

namespace Clinic.Web.Areas.Appointments.Visits.Pages
{
    public partial class Details : BlazorPage
    {
        private VisitDetail Item = new VisitDetail();
        private DialogBoxComponent DialogBox;
        private VisitInfo info;

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
                await AppointmentsServices.VisitService.DeleteAsync(new(context.TagId));
                if (OnDeleteClick.HasDelegate)
                    await OnDeleteClick.InvokeAsync(null);
          
                NavigationManager.NavigateTo($"Appointments/Visits/Index?HighlightId={Guid.Empty}");
            }
        }

        private string? ProcedureSaveError;
        private async Task SaveProcedureAsync(ProcedureSummary procedure)
        {
            ProcedureSaveError = null;
            var procedures = Item.ProcedureList
                .Select(CopyProcedure)
                .ToList();

            if (IsAddingProcedure)
            {
                procedures.Add(CopyProcedure(procedure));
            }
            else if (SelectedProcedureIndex >= 0 && SelectedProcedureIndex < procedures.Count)
            {
                procedures[SelectedProcedureIndex] = CopyProcedure(procedure);
            }
            else
            {
                ProcedureSaveError = "The selected procedure could not be found.";
                return;
            }

            if (await SaveProceduresAsync(procedures))
                await procedureModal.Hide();
        }

        protected override async Task OnParametersSetAsync()
        {
            await Open(Id);
        }

        public async Task Open(Guid id)
        {
            if (id == Guid.Empty) return;
            Id = id;

            Logger.LogInformation("Now loading Visit... {Id}", Id);

            var response = await AppointmentsServices.VisitService.GetAsync(new(Id));
            if (response != null && response.Succeed)
                Item = response.Data;
            await info.Open(Item);
            
            StateHasChanged();
        }


        string selectedTab = "More";
        private Task OnSelectedTabChanged(string name)
        {
            selectedTab = name;
            return Task.CompletedTask;
        }

        #region Procedures
        private Modal procedureModal;
        private ProcedureForm procedureForm;
        private ProcedureSummary selectedProcedure;
        private int SelectedProcedureIndex = -1;
        private bool IsAddingProcedure;

        private async Task AddProcedure()
        {
            IsAddingProcedure = true;
            SelectedProcedureIndex = -1;
            ProcedureSaveError = null;
            selectedProcedure = new ProcedureSummary
            {
                Id = Item.ProcedureList.Count == 0 ? 1 : Item.ProcedureList.Max(procedure => procedure.Id) + 1
            };
            await procedureModal.Show();
        }

        private async Task EditProcedure(int index)
        {
            if (index < 0 || index >= Item.ProcedureList.Count)
                return;

            IsAddingProcedure = false;
            SelectedProcedureIndex = index;
            ProcedureSaveError = null;
            selectedProcedure = CopyProcedure(Item.ProcedureList[index]);
            await procedureModal.Show();
        }

        private async Task RemoveProcedure(int index)
        {
            if (index < 0 || index >= Item.ProcedureList.Count)
                return;

            ProcedureSaveError = null;
            var procedures = Item.ProcedureList
                .Select(CopyProcedure)
                .ToList();
            procedures.RemoveAt(index);
            await SaveProceduresAsync(procedures);
        }

        private async Task<bool> SaveProceduresAsync(List<ProcedureSummary> procedures)
        {
            var response = await AppointmentsServices.VisitService.UpdateAsync(new UpdateVisitRequest
            {
                Id = Item.Id,
                Date = Item.Date,
                Physician = Item.Physician,
                PresentIllness = Item.PresentIllness,
                PatientId = Item.PatientId,
                Procedures = procedures
            });

            if (response == null || !response.Succeed || response.Data == null)
            {
                ProcedureSaveError = response?.Error ?? "The procedure changes could not be saved.";
                return false;
            }

            Item.ProcedureList = response.Data.ProcedureList;
            StateHasChanged();
            return true;
        }

        private static ProcedureSummary CopyProcedure(ProcedureSummary procedure)
        {
            return new ProcedureSummary
            {
                Id = procedure.Id,
                Name = procedure.Name,
                Description = procedure.Description
            };
        }

        #endregion
    }
}
