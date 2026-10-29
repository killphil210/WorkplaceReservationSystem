using Microsoft.AspNetCore.Components;
using System.Diagnostics;
using WorkplaceReservationSystem.ViewModels;

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