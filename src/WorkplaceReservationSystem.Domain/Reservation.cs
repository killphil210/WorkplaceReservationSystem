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

        public Reservation(DateOnly Date, TimeOnly StartTime, TimeOnly EndTime, int Id)
        {
            this.Date = Date;
            this.StartTime = StartTime;
            this.EndTime = EndTime;
            this.Id = Id;
        }

        public int Id { get; set; }
        public DateOnly? Date { get; set; }
        public TimeOnly? StartTime { get; set; }
        public TimeOnly? EndTime { get; set; }

        public bool IsComplete
        {
            get
            {
                return Date.HasValue && StartTime.HasValue && EndTime.HasValue;
            }
        }
    }
}
