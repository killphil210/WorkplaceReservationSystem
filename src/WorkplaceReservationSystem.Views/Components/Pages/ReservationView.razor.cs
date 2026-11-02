using Microsoft.AspNetCore.Components;
using System.Diagnostics;
using WorkplaceReservationSystem.ViewModels;
using WorkplaceReservationSystem.Views.Components.Events;

namespace WorkplaceReservationSystem.Views.Components.Pages
{
    public partial class ReservationView
    {
        [Parameter]
        public ReservationViewModel DataContext { get; set; }

        public void OnReservationDraftChanged()
        {
            DataContext.UpdateAvailableWorkplacesCommand.Execute(null);
        }

        public void OnReservationEntryAction((ReservationAction.ActionType actionType, int reservationId) action)
        {            
            switch (action.actionType)
            {
                case ReservationAction.ActionType.Edit:
                    
                    break;
                case ReservationAction.ActionType.Delete:
                    DataContext.DeleteReservationCommand.Execute(action.reservationId);
                    break;
            }
        }       

        protected override void OnInitialized()
        {
            DataContext.MainViewModel.OnChange += Refresh;
        }

        private void Refresh()
        {
            InvokeAsync(StateHasChanged);
        }

        public void Dispose()
        {
            DataContext.MainViewModel.OnChange -= Refresh;
        }
    }
}