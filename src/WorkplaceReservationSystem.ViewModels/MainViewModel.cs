using WorkplaceReservationSystem.Application.Services;
using WorkplaceReservationSystem.Domain;

namespace WorkplaceReservationSystem.ViewModels
{
    public class MainViewModel
    {
        public MainViewModel(ReservationService reservationService, UserService userService, WorkspaceService workspaceService)
        {
            _reservationService = reservationService;
            _userService = userService;
            _workspaceService = workspaceService;
            
            InitializeViewModels();
            CreateNewReservationDraft();
        }

        private readonly ReservationService _reservationService;
        private readonly UserService _userService;
        private readonly WorkspaceService _workspaceService;
        

        public event Action OnChange;

        public ReservationViewModel ReservationViewModel { get; set; }
        public WorkspaceViewModel WorkspaceViewModel { get; set; }

        public Reservation ReservationDraft;


        public DateOnly? ReservationDraftDate 
        { 
            get
            {
                return DateOnly.FromDateTime(ReservationDraft.Start.Value);
            }
            set
            {
                ReservationDraft.Start = new DateTime(value.Value.Year, value.Value.Month, value.Value.Day, ReservationDraft.Start.Value.Hour, ReservationDraft.Start.Value.Minute, 0);
                ReservationDraft.End = new DateTime(value.Value.Year, value.Value.Month, value.Value.Day, ReservationDraft.End.Value.Hour, ReservationDraft.End.Value.Minute, 0);
            }
        }

        public TimeOnly? ReservationDraftStartTime 
        { 
            get
            {
                return TimeOnly.FromDateTime(ReservationDraft.Start.Value);
            }
            set
            {
                ReservationDraft.Start = new DateTime(ReservationDraft.Start.Value.Year, ReservationDraft.Start.Value.Month, ReservationDraft.Start.Value.Day, value.Value.Hour, value.Value.Minute, 0);
            }
        }

        public TimeOnly? ReservationDraftEndTime
        {
            get
            {
                return TimeOnly.FromDateTime(ReservationDraft.End.Value);
            }
            set
            {
                ReservationDraft.End = new DateTime(ReservationDraft.End.Value.Year, ReservationDraft.End.Value.Month, ReservationDraft.End.Value.Day, value.Value.Hour, value.Value.Minute, 0);
            }
        }

        public HashSet<int> AvailableWorkplaces
        {
            get
            {
                return _workspaceService.GetAvailability(ReservationDraft);   
            }
        }

        public void SelectWorkplace(int id)
        {
            if (ReservationDraft.WorkplaceId != id)
            {
                ReservationDraft.WorkplaceId = id;
            }
            else
            {
                ReservationDraft.WorkplaceId = 0;
            }

            Update();
        }

        public void Update()
        {
            OnChange?.Invoke();
        }

        public void CreateNewReservationDraft()
        {
            ReservationDraft = new Reservation
            {
                UserId = _userService.CurrentUserId,                
                Start = DateTime.Now,
                End = DateTime.Now.AddHours(1)
            };                
        }

    private void InitializeViewModels()
        {
            ReservationViewModel = new ReservationViewModel(this, _reservationService, _userService, _workspaceService);
            WorkspaceViewModel = new WorkspaceViewModel(this, _workspaceService, _reservationService, _userService);
        }
    }
}
