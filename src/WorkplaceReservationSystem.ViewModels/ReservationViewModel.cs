using CommunityToolkit.Mvvm.Input;
using System.Windows.Input;
using WorkplaceReservationSystem.Application.Services;
using WorkplaceReservationSystem.Domain;

namespace WorkplaceReservationSystem.ViewModels
{
    public class ReservationViewModel
    {
        public ReservationViewModel(ReservationService reservationService, WorkspaceService workspaceService)
        {
            _workspaceService = workspaceService;

            InitializeCommands();
            ReservationService = new ReservationService();
            TempReservation = new Reservation(DateOnly.FromDateTime(DateTime.Now), new TimeOnly(8, 0), new TimeOnly(17, 0));
        }

        private readonly WorkspaceService _workspaceService;

        public ReservationService ReservationService { get; }

        public Reservation TempReservation { get; set; }

        #region Commands
        public ICommand CreateReservationCommand { get; set; }
        public ICommand ValidateReservationCommand { get; set; }
        #endregion

        public void CreateReservation()
        {
            //_reservationService.CreateReservation();
        }

        public void ValidateReservation()
        {
            ReservationService.ValidateReservation();
        }

        private void InitializeCommands()
        {
            CreateReservationCommand = new RelayCommand(CreateReservation);
            ValidateReservationCommand = new RelayCommand(ValidateReservation);
        }
    }
}
