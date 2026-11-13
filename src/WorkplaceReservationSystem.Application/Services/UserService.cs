using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkplaceReservationSystem.Application.Infrastructure;
using WorkplaceReservationSystem.Domain;

namespace WorkplaceReservationSystem.Application.Services
{
    public class UserService
    {
        public UserService(WorkplaceReservationDbContext dbContext)
        {
            _dbContext = dbContext;

            InitializeDB();
        }

        private readonly WorkplaceReservationDbContext _dbContext;


        public void InitializeDB()
        {
            if (!_dbContext.Users.Any())
            {                
                _dbContext.Users.Add(new User("Oliver", "Alt"));
                _dbContext.Users.Add(new User("Winand", "Debertin"));
                _dbContext.Users.Add(new User("Philipp", "Gierhardt"));
                _dbContext.Users.Add(new User("Marcel", "Neumann"));
                _dbContext.Users.Add(new User("Kerim Sevket", "Öge"));
                _dbContext.Users.Add(new User("Vasilios", "Paltsanitidis"));
                _dbContext.Users.Add(new User("Tyler", "Wolf"));

                _dbContext.SaveChanges();
            }
        }
    }
}
