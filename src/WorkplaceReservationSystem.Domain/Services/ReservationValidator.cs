using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkplaceReservationSystem.Domain.Services
{
    public class ReservationValidator
    {
        public ValidationResult Validate(Reservation reservation, Workplace workplace)
        {
            ValidationResult result = new ValidationResult();

            ValidationResult dateValid = ValidateDate(reservation);
            ValidationResult timeValid = ValidateTime(reservation);

            foreach (Reservation workplaceReservation in workplace.Reservations)
            {
                //ValidateDate();
            }

            return result;
        }

        private ValidationResult ValidateTime(Reservation reservation)
        {
            ValidationResult result = new ValidationResult();
            result.IsValid = true;

            if (reservation.StartTime >= reservation.EndTime)
            {
                result.IsValid = false;
                result.Error = "EndTime must be after StartTime.";
            }

            return result;
        }

        private ValidationResult ValidateDate(Reservation reservation)
        {
            ValidationResult result = new ValidationResult();
            result.IsValid = true;

            if (reservation.Date < DateOnly.FromDateTime(DateTime.Now))
            {
                result.IsValid = false;
                result.Error = "Date cannot be in the past.";
            }

            return result;
        }
    }
}
