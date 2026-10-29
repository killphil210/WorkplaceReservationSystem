using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkplaceReservationSystem.Domain
{
    public class ValidationResult
    {
        public ValidationResult()
        {
        }

        public ValidationResult(bool isValid, string error)
        {
            IsValid = isValid;
            ErrorMessage = error;
        }

        public bool IsValid;
        public string ErrorMessage;
    }
}
