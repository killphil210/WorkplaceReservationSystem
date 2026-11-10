using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
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

        public event Action OnChange;

        public ReservationViewModel ReservationViewModel { get; set; }
        public WorkspaceViewModel WorkspaceViewModel { get; set; }

        public Reservation ReservationDraft { get; set; } = new Reservation
        {
            Date = DateOnly.FromDateTime(DateTime.Now),
            StartTime = TimeOnly.FromDateTime(DateTime.Now),
            EndTime = TimeOnly.FromDateTime(DateTime.Now.AddHours(1)),
        };

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

            foreach (Workplace workplace in _workspaceService.GetWorkplaces())
            {
                result.Add(workplace.Id);
            }

            return result;
        }

        public void SelectWorkplace(int? id)
        {
            if (ReservationDraft.WorkplaceId != id)
            {
                ReservationDraft.WorkplaceId = id;
            }
            else
            {
                ReservationDraft.WorkplaceId = null;
            }

            Update();
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
