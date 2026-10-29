using System;
using System.Collections.Generic;
using System.Diagnostics;
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

            result = ValidateCompleteness(reservation);

            if (!result.IsValid)
            {
                return result;
            }

            result = ValidateDate(reservation);

            if (!result.IsValid)
            {
                return result;
            }
            
            result = ValidateTime(reservation);

            if (!result.IsValid)
            {
                return result;
            }
            
            result = ValidateOverlap(reservation, workplace);           

            return result;
        }

        private ValidationResult ValidateCompleteness(Reservation reservation)
        {
            ValidationResult result = new ValidationResult();

            result.IsValid = true;

            if (!reservation.IsComplete)
            {
                result.IsValid = false;
                result.ErrorMessage = "Die Reservierung ist unvollständig.";
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
                result.ErrorMessage = "Die Startzeit muss vor der Endzeit liegen.";
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
                result.ErrorMessage = "Das Datum darf nicht in der Vergangenheit liegen.";
            }

            return result;
        }

        private ValidationResult ValidateOverlap(Reservation reservation, Workplace workplace)
        {
            ValidationResult result = new ValidationResult();
            result.IsValid = true;

            foreach (Reservation workplaceReservation in workplace.Reservations)
            {                
                if (!(reservation.StartTime >= workplaceReservation.EndTime || reservation.EndTime <= workplaceReservation.StartTime))
                {
                    if (reservation.Date == workplaceReservation.Date)
                    {
                        result.IsValid = false;
                        result.ErrorMessage = "Der ausgewählte Arbeitsplatz ist im gewünschten Zeitraum belegt.";
                        break;
                    }
                }             
            }

            return result;
        }
    }
}
