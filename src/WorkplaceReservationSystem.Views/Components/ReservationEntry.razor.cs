using Microsoft.AspNetCore.Components;
using WorkplaceReservationSystem.Application.Services;
using WorkplaceReservationSystem.Domain;
using WorkplaceReservationSystem.Views.Components.Events;

namespace WorkplaceReservationSystem.Views.Components
{
    public partial class ReservationEntry
    {
        [Parameter]
        public EventCallback<(ReservationAction.ActionType, int)> OnAction { get; set; }

        [Parameter]
        public Reservation Reservation { get; set; }

        public void OnEditButtonClicked()
        {
            OnAction.InvokeAsync((ReservationAction.ActionType.Edit, Reservation.Id));
        }

        public void OnDeleteButtonClicked(int Id)
        {
            OnAction.InvokeAsync((ReservationAction.ActionType.Delete, Reservation.Id));
        }                
    }
}