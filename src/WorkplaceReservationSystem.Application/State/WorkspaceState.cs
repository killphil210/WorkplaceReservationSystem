using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkplaceReservationSystem.Domain;

namespace WorkplaceReservationSystem.Application.State
{
    public class WorkspaceState
    {
        public WorkspaceState() 
        {
            Initialize();
        }

        public Workspace Workspace { get; set; }

        public void Initialize()
        {
            Workspace = new Workspace(1, "Stillarbeitsraum");            
        }
    }
}
