using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WorkplaceReservationSystem.Application.Services;
using WorkplaceReservationSystem.Application.State;
using WorkplaceReservationSystem.Domain;

namespace WorkplaceReservationSystem.ViewModels
{
    public class MainViewModel
    {
        public MainViewModel(WorkspaceService workspaceService, ReservationService reservationService)
        {
            _workspaceService = workspaceService;
            _reservationService = reservationService;

            InitializeViewModels();
        }

        private readonly WorkspaceService _workspaceService;
        private readonly ReservationService _reservationService;
        
        private int? _selectedWorkplaceId = null;

        public event Action OnChange;

        public ReservationViewModel ReservationViewModel { get; set; }
        public WorkspaceViewModel WorkspaceViewModel { get; set; }

        public Reservation ReservationDraft { get; set; } = new Reservation
        {
            Date = DateOnly.FromDateTime(DateTime.Now),
            StartTime = TimeOnly.FromDateTime(DateTime.Now),
            EndTime = TimeOnly.FromDateTime(DateTime.Now.AddHours(1)),
        };
        

        public int? SelectedWorkplaceId 
        { 
            get
            {
                return _selectedWorkplaceId;
            }
            set
            {                
                _selectedWorkplaceId = value;
                OnChange?.Invoke();                                
            }
        }

        public HashSet<int> AvailableWorkplaces
        {
            get
            {
                if (_reservationService.ValidateReservation(ReservationDraft).IsValid)
                {
                    return _workspaceService.GetAvailability(ReservationDraft);                    
                }
                else
                {
                    return GetWorkplaceIds();
                }
            }
        }

        private HashSet<int> GetWorkplaceIds()
        {
            HashSet<int> result = new HashSet<int>();

            foreach (Workplace workplace in _workspaceService.GetWorkspace().Workplaces)
            {
                result.Add(workplace.Id);
            }

            return result;
        }

        public void SelectWorkplace(int id)
        {
            if (SelectedWorkplaceId != id)
            {
                SelectedWorkplaceId = id;
            }
            else
            {
                SelectedWorkplaceId = null;
            }
        }

        public void Update()
        {
            OnChange?.Invoke();
        }

        private void InitializeViewModels()
        {
            ReservationViewModel = new ReservationViewModel(this, _reservationService, _workspaceService);
            WorkspaceViewModel = new WorkspaceViewModel(this, _workspaceService);
        }
    }
}
