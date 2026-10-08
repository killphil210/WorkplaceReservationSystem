using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkplaceReservationSystem.Application.Services;
using WorkplaceReservationSystem.Application.State;

namespace WorkplaceReservationSystem.ViewModels
{
    public class MainViewModel
    {
        public MainViewModel(WorkspaceService workspaceService, WorkspaceState workspaceState)
        {
            _workspaceService = workspaceService;
            _workspaceState = workspaceState;
            InitializeViewModels();
        }

        private readonly WorkspaceService _workspaceService;
        private readonly WorkspaceState _workspaceState;

        public ReservationViewModel ReservationViewModel { get; set; }

        private void InitializeViewModels()
        {
            ReservationViewModel = new ReservationViewModel();
        }

    }
}
