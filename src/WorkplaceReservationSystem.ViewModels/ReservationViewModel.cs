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
            WorkspaceService = workspaceService;
            MainViewModel = mainViewModel;

            InitializeCommands();       
        }

        public WorkspaceService WorkspaceService { get; }

        private readonly ReservationService _reservationService;

        public MainViewModel MainViewModel { get; }

        public ValidationResult ValidationResult { get; set; } = new ValidationResult();

        #region Commands
        public ICommand UpdateAvailableWorkplacesCommand { get; set; }
        public ICommand TryCreateReservationCommand { get; set; }
        public ICommand EditReservationCommand { get; set; }
        public ICommand DeleteReservationCommand { get; set; }
        #endregion

        public void UpdateAvailableWorkplaces()
        {
            MainViewModel.Update();
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

        public void EditReservation(int reservationId)
        {
            //MainViewModel.EditReservation(reservationId);
        }

        public void DeleteReservation(int reservationId)
        {
            _reservationService.DeleteReservation(reservationId);
            MainViewModel.Update();
        }
    
        private void InitializeCommands()
        {
            UpdateAvailableWorkplacesCommand = new RelayCommand(UpdateAvailableWorkplaces);
            TryCreateReservationCommand = new RelayCommand(TryCreateReservation, CanExecuteTryCreateReservation);
            EditReservationCommand = new RelayCommand<int>(EditReservation);
            DeleteReservationCommand = new RelayCommand<int>(DeleteReservation);
        }
    }
}
