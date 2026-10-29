using Microsoft.AspNetCore.Components;
using System.Diagnostics;
using WorkplaceReservationSystem.Domain;
using WorkplaceReservationSystem.ViewModels;

namespace WorkplaceReservationSystem.Views.Components.Pages
{
    public partial class WorkspaceView
    {
        [Parameter]
        public WorkspaceViewModel DataContext { get; set; }

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


        public void OnDeskClicked(int workplaceId)
        {
            DataContext.ChangeSelectedWorkplaceCommand.Execute(workplaceId);
        }
    }
}