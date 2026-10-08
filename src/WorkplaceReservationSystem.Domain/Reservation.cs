using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkplaceReservationSystem.Domain
{
    public class Reservation
    {
        public Reservation()
        {

        }

        public Reservation(DateOnly Date, TimeOnly StartTime, TimeOnly EndTime)
        {
            this.Date = Date;
            this.StartTime = StartTime;
            this.EndTime = EndTime;
        }
        public DateOnly Date { get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
    }
}
