using WorkplaceReservationSystem.Domain;
using WorkplaceReservationSystem.Domain.Services;

namespace WorkplaceReservationSystem.Application.Services
{
    public class ReservationService
    {
        private readonly ReservationValidator _reservationValidator = new ReservationValidator();

        public void ValidateReservation(Reservation Reservation, Workplace Workplace)
        {
            _reservationValidator.Validate(Reservation, Workplace);
        }

        /*
        needs to call a domain service method to validate reservations
        needs to add reservation to the workplace
         */
    }
}
