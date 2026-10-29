using WorkplaceReservationSystem.Application.State;
using WorkplaceReservationSystem.Domain;
using WorkplaceReservationSystem.Domain.Services;

namespace WorkplaceReservationSystem.Application.Services
{
    public class WorkspaceService
    {
        private readonly WorkspaceState _workspaceState = new WorkspaceState();
        private readonly ReservationValidator _reservationValidator = new ReservationValidator();

        public Workspace GetWorkspace()
        {
            return _workspaceState.Workspace;
        }

        public Workplace? GetWorkplaceById(int workplaceId)
        {
            foreach (Workplace workplace in _workspaceState.Workspace.Workplaces)
            {
                if (workplace.Id == workplaceId)
                {
                    return workplace;
                }            
            }
            
            return null;
        }

        public HashSet<int> GetAvailability(Reservation reservationDraft)
        {
            HashSet<int> result = new HashSet<int>();

            foreach (Workplace workplace in _workspaceState.Workspace.Workplaces)
            {
                ValidationResult validationResult = _reservationValidator.Validate(reservationDraft, workplace);
                
                if (validationResult.IsValid)
                {
                    result.Add(workplace.Id);
                }
            }

            return result;
        }
    }
}
