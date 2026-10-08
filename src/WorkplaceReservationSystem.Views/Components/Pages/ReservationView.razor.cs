using Microsoft.AspNetCore.Components;
using WorkplaceReservationSystem.ViewModels;

namespace WorkplaceReservationSystem.Views.Components.Pages
{
    public partial class ReservationView
    {
        [Parameter]
        public ReservationViewModel DataContext { get; set; }
    }
}