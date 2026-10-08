using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkplaceReservationSystem.Domain
{
    public class Workplace
    {
        public Workplace() { }
        public Workplace(int id, string name) 
        {
            Id = id;
            Name = name;
        }

        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public bool IsReserved 
        { 
            get
            {
                return Reservation != null;
            }
        }

        public Reservation Reservation { get; set; }

        public List<Reservation> Reservations { get; set; } = new List<Reservation>();
    }
}
