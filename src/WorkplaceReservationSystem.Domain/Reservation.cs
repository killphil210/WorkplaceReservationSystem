using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace WorkplaceReservationSystem.Domain
{
    public class Reservation
    {
        public Reservation()
        {

        }

        public Reservation(DateOnly Date, TimeOnly StartTime, TimeOnly EndTime, int id, int workplaceId)
        {
            this.Date = Date;
            this.StartTime = StartTime;
            this.EndTime = EndTime;
            this.Id = id;
            this.WorkplaceId = workplaceId;
        }

        public int Id { get; set; }
        public int? WorkplaceId { get; set; }
        public int UserId { get; set; }

        public DateOnly? Date { get; set; }
        public TimeOnly? StartTime { get; set; }
        public TimeOnly? EndTime { get; set; }

        public bool IsComplete
        {
            get
            {
                return Date.HasValue && StartTime.HasValue && EndTime.HasValue && WorkplaceId.HasValue;
            }
        }

        public bool HasTimeframe
        {
            get
            {
                return Date.HasValue && StartTime.HasValue && EndTime.HasValue;
            }            
        }

        public Reservation Copy()
        {
            Reservation result = new Reservation();

            result.Id = this.Id;
            result.WorkplaceId = this.WorkplaceId;
            result.UserId = this.UserId;
            result.Date = this.Date;
            result.StartTime = this.StartTime;
            result.EndTime = this.EndTime;

            return result;
        }
    }
}
