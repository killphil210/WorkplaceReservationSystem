using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkplaceReservationSystem.Domain
{
    public class Workspace
    {
        public Workspace() { }

        public Workspace(int id, string name) 
        { 
            Id = id;
            Name = name;
        }

        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;


        public List<Workplace> Workplaces = new List<Workplace>();
    }
}
