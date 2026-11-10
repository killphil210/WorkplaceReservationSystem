using CommunityToolkit.Mvvm.Input;
using System.Diagnostics;
using System.Windows.Input;
using WorkplaceReservationSystem.Application.Services;
using WorkplaceReservationSystem.Domain;

namespace WorkplaceReservationSystem.ViewModels
{
    public class ReservationViewModel
    {
        public ReservationViewModel(MainViewModel mainViewModel, ReservationService reservationService, WorkspaceService workspaceService)
        {
            ReservationService = reservationService;
            WorkspaceService = workspaceService;
            MainViewModel = mainViewModel;

            InitializeCommands();
        }

        public ReservationService ReservationService { get; }

        public bool IsEditMode { get; set; } = false;

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
            MainViewModel.Update();
        }

        public void TryCreateReservation()
        {
            ValidationResult = ReservationService.TryCreateReservation(MainViewModel.ReservationDraft);

            if (ValidationResult.IsValid == true)
            {
                MainViewModel.ReservationDraft.WorkplaceId = null;
            }

            MainViewModel.Update();
        }

        public bool CanExecuteTryCreateReservation()
        {
            bool result = true;

            if (!MainViewModel.ReservationDraft.IsComplete)
            {
                result = false;
            }

            return result;
        }

        public void StartEditingReservation(int reservationId)
        {
            Reservation reservation = ReservationService.GetReservationById(reservationId);

            ReservationBeingEdited = reservation;
            MainViewModel.ReservationDraft = reservation.Copy();

            ReservationService.DeleteReservation(reservationId);
            IsEditMode = true;
            MainViewModel.Update();             
        }

        public void SaveEditedReservation()
        {
            if (ReservationBeingEdited != null)
            {
                ReservationBeingEdited = null;
                ReservationService.TryCreateReservation(MainViewModel.ReservationDraft);
            }

            IsEditMode = false;
            MainViewModel.SelectWorkplace(null);
        }

        public bool CanExecuteSaveEditedReservation()
        {
            bool result = true;

            if (!MainViewModel.ReservationDraft.IsComplete)
            {
                result = false;
            }

            return result;
        }

        public void CancelEditingReservation()
        {
            if (ReservationBeingEdited != null)
            {
                MainViewModel.ReservationDraft = ReservationBeingEdited;
                ReservationBeingEdited = null;
                ReservationService.TryCreateReservation(MainViewModel.ReservationDraft);
            }

            IsEditMode = false;
            MainViewModel.SelectWorkplace(null);            
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
