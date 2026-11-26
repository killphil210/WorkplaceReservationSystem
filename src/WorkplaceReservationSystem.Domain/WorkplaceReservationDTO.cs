using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkplaceReservationSystem.Domain
{
    public class WorkplaceReservationDTO
    {
        public Reservation Reservation { get; set; }
        public User User { get; set; }

        public string UserFullName
        {
            get
            {
                return $"{User.FirstName} {User.LastName}";
            }
        }

        public string UserInitials 
        { 
            get
            {
                return $"{User.FirstName[0]}{User.LastName[0]}";
            }
        }
        public string DisplayString 
        {
            get
            {
                return $"{UserFullName} ({Reservation.Start.Value.ToString("HH:mm")} - {Reservation.End.Value.ToString("HH:mm")})";
            }
        }

    }
}
