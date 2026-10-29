using CommunityToolkit.Mvvm.Input;
using System.Windows.Input;
using WorkplaceReservationSystem.Application.Services;
using WorkplaceReservationSystem.Domain;

namespace WorkplaceReservationSystem.ViewModels
{
    public class ReservationViewModel
    {
        public ReservationViewModel(MainViewModel mainViewModel, ReservationService reservationService, WorkspaceService workspaceService)
        {
            _reservationService = reservationService;
            _workspaceService = workspaceService;
            MainViewModel = mainViewModel;

            InitializeCommands();       
        }

        private readonly WorkspaceService _workspaceService;

        private readonly ReservationService _reservationService;

        public MainViewModel MainViewModel { get; }

        public ValidationResult ValidationResult { get; set; } = new ValidationResult();

        #region Commands
        public ICommand UpdateAvailableWorkplacesCommand { get; set; }
        public ICommand TryCreateReservationCommand { get; set; }
        #endregion

        public void UpdateAvailableWorkplaces()
        {
            //MainViewModel.Update();
        }

        public void TryCreateReservation()
        {
            ValidationResult = _reservationService.TryCreateReservation(MainViewModel.ReservationDraft, MainViewModel.SelectedWorkplaceId.Value);

            if (ValidationResult.IsValid == true)
            {
                MainViewModel.SelectedWorkplaceId = null;
            }

            MainViewModel.Update();
        }

        public bool CanExecuteTryCreateReservation()
        {
            bool result = true;

            if (MainViewModel.SelectedWorkplaceId == null)
            {
                result = false;
            }

            return result;
        }

        private void InitializeCommands()
        {
            UpdateAvailableWorkplacesCommand = new RelayCommand(UpdateAvailableWorkplaces);
            TryCreateReservationCommand = new RelayCommand(TryCreateReservation, CanExecuteTryCreateReservation);
        }
    }
}
