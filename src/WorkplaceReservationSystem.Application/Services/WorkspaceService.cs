using WorkplaceReservationSystem.Application.Infrastructure;
using WorkplaceReservationSystem.Application.State;
using WorkplaceReservationSystem.Domain;
using WorkplaceReservationSystem.Domain.Services;

namespace WorkplaceReservationSystem.Application.Services
{
    public class WorkspaceService
    {
        public WorkspaceService(WorkplaceReservationDbContext dbContext, ReservationService reservationService)
        {
            _dbContext = dbContext;
            _reservationService = reservationService;

            InitializeDB();
        }

        private readonly ReservationService _reservationService;
        private readonly WorkplaceReservationDbContext _dbContext;


        public List<Workplace> GetWorkplaces()
        {
            return _dbContext.Workplaces.ToList();
        }

        public Workplace GetWorkplaceById(int workplaceId)
        {
            return _dbContext.Workplaces.FirstOrDefault(workplace => workplace.Id == workplaceId);
        }

        public HashSet<int> GetAvailability(Reservation reservationDraft)
        {
            HashSet<int> result = new HashSet<int>();

            foreach (Workplace workplace in _dbContext.Workplaces)
            {
                ValidationResult validationResult = _reservationService.ValidateWorkplaceAvailability(reservationDraft, workplace.Id);

                if (validationResult.IsValid)
                {
                    result.Add(workplace.Id);
                }
            }
            
            return result;
        }

        public void InitializeDB()
        {
            if (!_dbContext.Workplaces.Any())
            {
                for (int i = 1; i <= 4; i++)
                {
                    _dbContext.Workplaces.Add(new Workplace($"AP_{i}"));
                }
                _dbContext.SaveChanges();
            }
        }

    }
}
