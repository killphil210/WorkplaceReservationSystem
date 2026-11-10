using System.Diagnostics;
using WorkplaceReservationSystem.Application.Infrastructure;
using WorkplaceReservationSystem.Domain;
using WorkplaceReservationSystem.Domain.Services;

namespace WorkplaceReservationSystem.Application.Services
{
    public class ReservationService
    {
        public ReservationService(WorkplaceReservationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        private readonly ReservationValidator _reservationValidator = new ReservationValidator();
        private readonly WorkplaceReservationDbContext _dbContext;

        private void CreateReservation(Reservation reservationDraft)
        {
            _dbContext.Reservations.Add(reservationDraft.Copy());
            _dbContext.SaveChanges();
        }

        public ValidationResult ValidateReservation(Reservation reservation)
        {           
            return _reservationValidator.Validate(reservation);
        }

        public ValidationResult ValidateWorkplaceAvailability(Reservation reservation, int workplaceId)
        {

            return _reservationValidator.Validate(reservation, GetReservationsByWorkplaceId(workplaceId));
        }

        public void DeleteReservation(int reservationId)
        {
            Reservation reservation = _dbContext.Reservations.FirstOrDefault(reservation => reservation.Id == reservationId);
            _dbContext.Reservations.Remove(reservation);
            _dbContext.SaveChanges();
        }

        public List<Reservation> GetReservations()
        {
            return _dbContext.Reservations.OrderBy(reservation => reservation.Date).ThenBy(reservation => reservation.StartTime).ToList();
        }

        public Reservation GetReservationById(int reservationId)
        {
            return _dbContext.Reservations.FirstOrDefault(reservation => reservation.Id == reservationId);
        }

        public List<Reservation> GetReservationsByWorkplaceId(int workplaceId)
        {
            return _dbContext.Reservations.Where(reservation => reservation.WorkplaceId == workplaceId).ToList();
        }

        public ValidationResult TryCreateReservation(Reservation reservation)
        {
            ValidationResult result = new ValidationResult();

            if (reservation.WorkplaceId == null)
            {
                result.IsValid = false;
                result.ErrorMessage = "Kein Arbeitsplatz ausgewählt.";
                Debug.WriteLine($"Validation failed: {result.ErrorMessage}");
                return result;
            }

            List<Reservation> workplaceReservations = GetReservationsByWorkplaceId(reservation.WorkplaceId.Value);

            result = _reservationValidator.Validate(reservation, workplaceReservations);

            if (result.IsValid)
            {
                CreateReservation(reservation);                
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
