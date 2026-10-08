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

            Initialize();
        }

        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;


        List<Workplace> Workplaces = new List<Workplace>();

        public void Initialize()
        {
            for (int i = 1; i < 4; i++)
            {
                Workplaces.Add(new Workplace(i, $"AP_{i}"));
            }
        }
    }
}
