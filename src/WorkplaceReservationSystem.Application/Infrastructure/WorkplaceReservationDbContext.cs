using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkplaceReservationSystem.Domain;

namespace WorkplaceReservationSystem.Application.Infrastructure
{
    public class WorkplaceReservationDbContext : DbContext
    {
        public WorkplaceReservationDbContext(DbContextOptions<WorkplaceReservationDbContext> options) : base(options)
        {
        }

        public DbSet<Workplace> Workplaces { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
    }
}
