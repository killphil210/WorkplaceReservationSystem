using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkplaceReservationSystem.Domain
{
    public class Workplace
    {
        public int Id { get; set; }
        public bool IsReserved 
        { 
            get
            {
                return Reservation != null;
            }
        }

        public Reservation Reservation { get; set; }
    }
}
