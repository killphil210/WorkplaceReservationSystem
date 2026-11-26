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

        public Reservation(DateTime Start, DateTime End, int id, int workplaceId)
        {            
            this.Start = Start;
            this.End = End;
            this.Id = id;
            this.WorkplaceId = workplaceId;
        }

        public int Id { get; set; }
        public int WorkplaceId { get; set; }
        public int UserId { get; set; }

        public DateTime? Start { get; set; }
        public DateTime? End { get; set; }

        public bool IsComplete
        {
            get
            {
                return Start.HasValue && End.HasValue && (WorkplaceId != 0);
            }
        }

        public bool HasTimeframe
        {
            get
            {
                return Start.HasValue && End.HasValue;
            }            
        }

        public Reservation Copy()
        {
            Reservation result = new Reservation();

            result.Id = this.Id;
            result.WorkplaceId = this.WorkplaceId;
            result.UserId = this.UserId;
            result.Start = this.Start;
            result.End = this.End;

            return result;
        }
    }
}
