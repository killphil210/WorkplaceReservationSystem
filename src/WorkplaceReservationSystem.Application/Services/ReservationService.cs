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

        public ValidationResult UpdateReservation(Reservation updatedDraft)
        {
            ValidationResult result = new ValidationResult();
            result = ValidateReservation(updatedDraft);

            if(!result.IsValid)
            {
                return result;
            }

            Reservation? existingReservation = GetReservationById(updatedDraft.Id);

            if(existingReservation == null)
            {
                result.IsValid = false;
                result.ErrorMessage = "Reservierung nicht gefunden";

                return result;
            }

            existingReservation.WorkplaceId = updatedDraft.WorkplaceId;
            existingReservation.Start = updatedDraft.Start;
            existingReservation.End = updatedDraft.End;

            _dbContext.SaveChanges();

            return result;
        }

        public ValidationResult ValidateReservation(Reservation reservation)
        {           
            return _reservationValidator.Validate(reservation);
        }

        public ValidationResult ValidateWorkplaceAvailability(Reservation reservation, int workplaceId)
        {

            return _reservationValidator.Validate(reservation, GetConflictingReservation(reservation, workplaceId));
        }

        public Reservation? GetConflictingReservation(Reservation reservationDraft, int workplaceId)
        {
            return _dbContext.Reservations.FirstOrDefault(reservation => reservation.WorkplaceId == workplaceId && reservation.Id != reservationDraft.Id && reservation.Start < reservationDraft.End && reservation.End > reservationDraft.Start);
        }

        public void DeleteReservation(int reservationId)
        {
            Reservation reservation = _dbContext.Reservations.FirstOrDefault(reservation => reservation.Id == reservationId);
            _dbContext.Reservations.Remove(reservation);
            _dbContext.SaveChanges();
        }

        public List<Reservation> GetReservations()
        {
            return _dbContext.Reservations.OrderBy(reservation => reservation.Start).ToList();
        }

        public Reservation GetReservationById(int reservationId)
        {
            return _dbContext.Reservations.FirstOrDefault(reservation => reservation.Id == reservationId);
        }

        public List<Reservation> GetReservationsByUserId(int userId)
        {
            return _dbContext.Reservations.Where(reservation => reservation.UserId == userId).ToList();
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
                       
            result = _reservationValidator.Validate(reservation, GetConflictingReservation(reservation, reservation.WorkplaceId));

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
