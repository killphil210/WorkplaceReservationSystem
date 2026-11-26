using CommunityToolkit.Mvvm.Input;
using System.Diagnostics;
using System.Linq;
using System.Windows.Input;
using WorkplaceReservationSystem.Application.Services;
using WorkplaceReservationSystem.Domain;

namespace WorkplaceReservationSystem.ViewModels
{
    public class ReservationViewModel
    {
        public ReservationViewModel(MainViewModel mainViewModel, ReservationService reservationService, UserService userService, WorkspaceService workspaceService)
        {
            ReservationService = reservationService;
            UserService = userService;
            WorkspaceService = workspaceService;
            MainViewModel = mainViewModel;

            InitializeCommands();
        }

        public ReservationService ReservationService { get; }

        public bool IsEditMode { get; set; } = false;

        public UserService UserService { get; }

        public WorkspaceService WorkspaceService { get; }


        public MainViewModel MainViewModel { get; }

        public ValidationResult ValidationResult { get; set; } = new ValidationResult();

        private Reservation ReservationBeingEdited;

        #region Commands
        public ICommand UpdateAvailableWorkplacesCommand { get; set; }
        public ICommand TryCreateReservationCommand { get; set; }
        public ICommand StartEditingReservationCommand { get; set; }
        public ICommand SaveEditedReservationCommand { get; set; }
        public ICommand CancelEditingReservationCommand { get; set; }
        public ICommand DeleteReservationCommand { get; set; }
        #endregion


        public void UpdateAvailableWorkplaces()
        {
            ValidationResult = ReservationService.ValidateReservation(MainViewModel.ReservationDraft);
            MainViewModel.Update();
        }

        public void TryCreateReservation()
        {
            ValidationResult = ReservationService.TryCreateReservation(MainViewModel.ReservationDraft);

            if (ValidationResult.IsValid == true)
            {
                MainViewModel.CreateNewReservationDraft();
            }

            MainViewModel.Update();
        }

        public bool CanExecuteTryCreateReservation()
        {            
            return MainViewModel.ReservationDraft.IsComplete && MainViewModel.AvailableWorkplaces.Contains(MainViewModel.ReservationDraft.WorkplaceId);            
        }

        public void StartEditingReservation(int reservationId)
        {
            MainViewModel.ReservationDraft = ReservationService.GetReservationById(reservationId).Copy(); 
            IsEditMode = true;
            MainViewModel.Update();             
        }

        public void SaveEditedReservation()
        {
            ReservationService.UpdateReservation(MainViewModel.ReservationDraft); 
            IsEditMode = false;
            MainViewModel.CreateNewReservationDraft();
            MainViewModel.Update();
        }

        public bool CanExecuteSaveEditedReservation()
        {
            return MainViewModel.ReservationDraft.IsComplete && MainViewModel.AvailableWorkplaces.Contains(MainViewModel.ReservationDraft.WorkplaceId);
        }

        public void CancelEditingReservation()
        {
            IsEditMode = false;
            MainViewModel.CreateNewReservationDraft();
            MainViewModel.Update();
        }

        public void DeleteReservation(int reservationId)
        {
            ReservationService.DeleteReservation(reservationId);
            MainViewModel.Update();
        }
        
    
        private void InitializeCommands()
        {
            UpdateAvailableWorkplacesCommand = new RelayCommand(UpdateAvailableWorkplaces);
            TryCreateReservationCommand = new RelayCommand(TryCreateReservation, CanExecuteTryCreateReservation);
            StartEditingReservationCommand = new RelayCommand<int>(StartEditingReservation);
            SaveEditedReservationCommand = new RelayCommand(SaveEditedReservation);
            CancelEditingReservationCommand = new RelayCommand(CancelEditingReservation);
            DeleteReservationCommand = new RelayCommand<int>(DeleteReservation);
        }
    }
}
