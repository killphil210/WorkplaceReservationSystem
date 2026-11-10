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
        public Workplace(string name) 
        {
            Name = name;
        }

        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}
