using System.Diagnostics;
using WorkplaceReservationSystem.Domain;
using WorkplaceReservationSystem.Domain.Services;

namespace WorkplaceReservationSystem.Application.Services
{
    public class ReservationService
    {
        public ReservationService(WorkspaceService workspaceService)
        {
            _workspaceService = workspaceService;
        }

        private readonly ReservationValidator _reservationValidator = new ReservationValidator();
        private readonly WorkspaceService _workspaceService;

        public ValidationResult ValidateReservation(Reservation Reservation, Workplace Workplace)
        {           
            return _reservationValidator.Validate(Reservation, Workplace);
        }

        public ValidationResult TryCreateReservation(Reservation reservation, int? selectedWorkplaceId)
        {
            ValidationResult result = new ValidationResult();

            if (selectedWorkplaceId == null)
            {
                result.IsValid = false;
                result.ErrorMessage = "Kein Arbeitsplatz ausgewählt.";
                Debug.WriteLine($"Validation failed: {result.ErrorMessage}");
                return result;
            }

            Workplace? SelectedWorkplace = _workspaceService.GetWorkplaceById(selectedWorkplaceId.Value);

            if (SelectedWorkplace == null)
            {
                throw new InvalidOperationException($"Workplace with ID {selectedWorkplaceId.Value} not found.");
            }

            result = ValidateReservation(reservation, SelectedWorkplace);

            if (result.IsValid)
            {
                SelectedWorkplace.Reservations.Add(reservation);

                result.IsValid = true;
                Debug.WriteLine($"Validation succeeded");
                return result;
            }
            else
            {
                Debug.WriteLine($"Validation failed: {result.ErrorMessage}");
                return result;
            }
        }
    }
}
