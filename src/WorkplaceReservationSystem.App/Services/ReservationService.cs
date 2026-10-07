using WorkplaceReservationSystem.Domain;

namespace WorkplaceReservationSystem.App.Services
{
    public class ReservationService
    {
        public DateOnly CurrentDate { get; set; }

        public TimeOnly CurrentStartTime { get; set; }
        public TimeOnly CurrentEndTime { get; set; }

        public Workplace CurrentWorkplace = new Workplace();

        /*
        needs to create a reservation
        needs to keep track of which workplace is currently clicked
        needs to call a domain service method to validate reservations
        needs to add reservation to the workplace
         */
    }
}
