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

        private int _lastReservationId;       

        private int GetNextReservationId()
        {
            int result;
                       
            _lastReservationId++;
            result = _lastReservationId;

            return result;            
        }

        public Reservation CreateReservation(Reservation reservationDraft)
        {
            Reservation result = new Reservation(reservationDraft.Date.Value, reservationDraft.StartTime.Value, reservationDraft.EndTime.Value, GetNextReservationId());

            return result;
        }

        public ValidationResult ValidateReservation(Reservation Reservation)
        {           
            return _reservationValidator.Validate(Reservation);
        }

        public void DeleteReservation(int reservationId)
        {
            foreach (Workplace workplace in _workspaceService.GetWorkspace().Workplaces)
            {
                foreach (Reservation reservation in workplace.Reservations)
                {
                    if (reservation.Id == reservationId)
                    {
                        workplace.Reservations.Remove(reservation);
                        return;
                    }
                }
            }
            throw new InvalidOperationException($"Reservation with ID {reservationId} not found.");
        }

        public Reservation? GetReservationById(int reservationId)
        {
            Reservation result = null;

            foreach (Workplace workplace in _workspaceService.GetWorkspace().Workplaces)
            {
                foreach (Reservation reservation in workplace.Reservations)
                {
                    if (reservation.Id == reservationId)
                    {
                        result = reservation;
                        return result;
                    }
                }
            }

            return result;
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

            result = _reservationValidator.Validate(reservation, SelectedWorkplace);

            if (result.IsValid)
            {
                SelectedWorkplace.Reservations.Add(CreateReservation(reservation));
                
                result.IsValid = true;

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
