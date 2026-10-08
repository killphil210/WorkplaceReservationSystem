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

        public List<Workspace> Workspaces { get; set; } = new List<Workspace>();

        public void Initialize()
        {
            Workspace workspace = new Workspace(1, "Stillarbeitsraum");
            Workspaces.Add(workspace);
        }
    }
}
