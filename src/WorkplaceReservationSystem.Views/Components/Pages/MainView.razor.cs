using Microsoft.AspNetCore.Components;
using System.Diagnostics;
using WorkplaceReservationSystem.ViewModels;

namespace WorkplaceReservationSystem.Views.Components.Pages
{
    public partial class MainView
    {
        [Inject]
        public MainViewModel DataContext { get; set; }
    }
}