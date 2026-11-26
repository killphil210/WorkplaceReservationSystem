using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WorkplaceReservationSystem.Application.Services;
using WorkplaceReservationSystem.Domain;

namespace WorkplaceReservationSystem.ViewModels
{
    public class WorkspaceViewModel
    {
        public WorkspaceViewModel(WorkspaceService workspaceService)
        {            
            InitializeCommands();
        }

        public WorkspaceViewModel(MainViewModel mainViewModel, WorkspaceService workspaceService, ReservationService reservationService, UserService userService)
        {
            MainViewModel = mainViewModel;
            WorkspaceService = workspaceService;
            ReservationService = reservationService;
            UserService = userService;

            InitializeCommands();
        }

        public WorkspaceService WorkspaceService {get; set; }
        public ReservationService ReservationService { get; set; }
        public UserService UserService { get; set; }

        public MainViewModel MainViewModel { get; }

        public ICommand ChangeSelectedWorkplaceCommand { get; set; }

        public void ExecuteChangeSelectedWorkplace(int workplaceId)
        {
            MainViewModel.SelectWorkplace(workplaceId);
        }

        public WorkplaceReservationDTO? GetWorkplaceReservation(Reservation reservationDraft, int workplaceId)
        {
            WorkplaceReservationDTO result = new WorkplaceReservationDTO();
            
            Reservation? conflictingReservation = ReservationService.GetConflictingReservation(reservationDraft, workplaceId);

            if(conflictingReservation == null)
            {
                return null;
            }

            result.Reservation = conflictingReservation;
            result.User = UserService.GetUserById(result.Reservation.UserId);

            return result;
        }

        private void InitializeCommands()
        {
            ChangeSelectedWorkplaceCommand = new RelayCommand<int>(ExecuteChangeSelectedWorkplace);
        }
    }
}
