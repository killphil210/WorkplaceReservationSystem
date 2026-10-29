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

            InitializeTestWorkplaces();
        }

        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;


        public List<Workplace> Workplaces = new List<Workplace>();

        public void InitializeTestWorkplaces()
        {
            for (int i = 1; i <= 4; i++)
            {
                Workplaces.Add(new Workplace(i, $"AP_{i}"));
            }
            
            Workplaces[0].Reservations
                .Add(new Reservation 
                {
                    Date = new DateOnly(2026, 11, 10),
                    StartTime = new TimeOnly(8, 0),
                    EndTime = new TimeOnly(10, 0)
                });

            Workplaces[0].Reservations
                .Add(new Reservation 
                {
                    Date = new DateOnly(2026, 11, 10),
                    StartTime = new TimeOnly(12, 0),
                    EndTime = new TimeOnly(14, 0)
                });

            Workplaces[0].Reservations
                .Add(new Reservation
                {

                    Date = new DateOnly(2026, 11, 11),
                    StartTime = new TimeOnly(9, 0),
                    EndTime = new TimeOnly(11, 0)
                });

            Workplaces[0].Reservations
                .Add(new Reservation
                {
                    Date = new DateOnly(2026, 11, 12),
                    StartTime = new TimeOnly(13, 0),
                    EndTime = new TimeOnly(16, 0)
                });

            Workplaces[1].Reservations
    .Add(new Reservation
    {
        Date = new DateOnly(2026, 11, 10),
        StartTime = new TimeOnly(10, 0),
        EndTime = new TimeOnly(12, 0)
    });

            Workplaces[1].Reservations
                .Add(new Reservation
                {
                    Date = new DateOnly(2026, 11, 10),
                    StartTime = new TimeOnly(14, 0),
                    EndTime = new TimeOnly(17, 0)
                });

            Workplaces[1].Reservations
                .Add(new Reservation
                {
                    Date = new DateOnly(2026, 11, 11),
                    StartTime = new TimeOnly(8, 0),
                    EndTime = new TimeOnly(9, 30)
                });

            Workplaces[1].Reservations
                .Add(new Reservation
                {
                    Date = new DateOnly(2026, 11, 13),
                    StartTime = new TimeOnly(11, 0),
                    EndTime = new TimeOnly(15, 0)
                });

            Workplaces[2].Reservations
    .Add(new Reservation
    {
        Date = new DateOnly(2026, 11, 10),
        StartTime = new TimeOnly(8, 0),
        EndTime = new TimeOnly(17, 0)
    });

            Workplaces[2].Reservations
                .Add(new Reservation
                {
                    Date = new DateOnly(2026, 11, 11),
                    StartTime = new TimeOnly(8, 0),
                    EndTime = new TimeOnly(17, 0)
                });

            Workplaces[2].Reservations
                .Add(new Reservation
                {
                    Date = new DateOnly(2026, 11, 12),
                    StartTime = new TimeOnly(8, 0),
                    EndTime = new TimeOnly(17, 0)
                });

            Workplaces[2].Reservations
                .Add(new Reservation
                {
                    Date = new DateOnly(2026, 11, 13),
                    StartTime = new TimeOnly(8, 0),
                    EndTime = new TimeOnly(17, 0)
                });

            Workplaces[3].Reservations
    .Add(new Reservation
    {
        Date = new DateOnly(2026, 11, 10),
        StartTime = new TimeOnly(9, 0),
        EndTime = new TimeOnly(10, 0)
    });

            Workplaces[3].Reservations
                .Add(new Reservation
                {
                    Date = new DateOnly(2026, 11, 10),
                    StartTime = new TimeOnly(11, 0),
                    EndTime = new TimeOnly(12, 0)
                });

            Workplaces[3].Reservations
                .Add(new Reservation
                {
                    Date = new DateOnly(2026, 11, 10),
                    StartTime = new TimeOnly(13, 0),
                    EndTime = new TimeOnly(14, 0)
                });

            Workplaces[3].Reservations
                .Add(new Reservation
                {
                    Date = new DateOnly(2026, 11, 10),
                    StartTime = new TimeOnly(15, 0),
                    EndTime = new TimeOnly(16, 0)
                });
        }
    }
}
