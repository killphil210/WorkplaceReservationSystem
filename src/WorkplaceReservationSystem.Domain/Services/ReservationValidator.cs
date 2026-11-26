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
        public ValidationResult Validate(Reservation reservation)
        {
            ValidationResult result = new ValidationResult();

            if (!reservation.HasTimeframe)
            {
                result.IsValid = false;
                result.ErrorMessage = "Die Reservierung ist unvollständig.";
                return result;
            }

            result = ValidateTime(reservation);

            if (!result.IsValid)
            {
                return result;
            }           

            return result;
        }

        public ValidationResult Validate(Reservation reservation, Reservation? conflictingReservation)
        {
            ValidationResult result = new ValidationResult();          
            result.IsValid = true;

            if (!reservation.HasTimeframe)
            {
                result.IsValid = false;
                result.ErrorMessage = "Die Reservierung ist unvollständig.";
                return result;
            }   
            
            result = ValidateTime(reservation);

            if (!result.IsValid)
            {
                return result;
            }
            
            result = ValidateOverlap(conflictingReservation);           

            return result;
        }

        private ValidationResult ValidateTime(Reservation reservation)
        {
            ValidationResult result = new ValidationResult();
            result.IsValid = true;

            if (reservation.Start <= DateTime.Now.AddMinutes(-5))
            {
                result.IsValid = false;
                result.ErrorMessage = "Die Startzeit darf nicht in der Vergangenheit liegen.";
                return result;
            }

            if (reservation.Start >= reservation.End)
            {
                result.IsValid = false;
                result.ErrorMessage = "Die Startzeit muss vor der Endzeit liegen.";
                return result;
            }

            return result;
        }

        private ValidationResult ValidateOverlap(Reservation? conflictingReservation)
        {
            ValidationResult result = new ValidationResult();
            result.IsValid = true;

            if (conflictingReservation != null)
            {
                result.IsValid = false;
                result.ErrorMessage = "Der ausgewählte Arbeitsplatz ist im gewünschten Zeitraum belegt.";
            }
            
            return result;
        }
    }
}
