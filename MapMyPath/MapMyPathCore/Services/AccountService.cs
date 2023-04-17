using MapMyPathCore.Data;
using MapMyPathLib;

namespace MapMyPathCore.Services
{
    public class AccountService
    {
        private MapMyPathCoreContext CONTEXT;
        public AccountService()
        {
            CONTEXT = new MapMyPathCoreContext(new Microsoft.EntityFrameworkCore.DbContextOptions<MapMyPathCoreContext>());
        }

        public IList<AppUser> GetUsers()
        {
            var users = CONTEXT.Users.Where(u => u.IsDeleted == 0).ToList();
            var modelUsers = new List<AppUser>();
            foreach (var item in users)
            {
                modelUsers.Add(new AppUser
                {
                    UserName = item.UserName,
                    Id = Guid.Parse(item.Id),
                    IsDeleted = item.IsDeleted,
                    Password = item.Password,
                    FirstName = item.FirstName,
                    LastName = item.LastName,


                });
            }
            return modelUsers;
        }

        public AppUser GetUserByUsername(string username)
        {
            var user = CONTEXT.Users.First(x => x.UserName == username);
            return new AppUser
            {
                Id=Guid.Parse(user.Id),
                FirstName = user.FirstName,
                LastName = user.LastName,
                CreatedAt = user.CreatedAt,
                IsDeleted = user.IsDeleted,
                Password = user.Password,
                UserName = user.UserName,

            };
        }

        public bool AddUser(AppUser user)
        {
            try
            {
                CONTEXT.Users.Add(new Areas.Identity.Data.MapMyPathCoreUser
                {
                    CreatedAt = user.CreatedAt,
                    Email = user.UserName,
                    EmailConfirmed = true,
                    FirstName = user.FirstName,
                    IsDeleted = 0,
                    LastName = user.LastName,
                    Password = user.Password,
                    UserName = user.UserName

                });
                CONTEXT.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool ExistsInDatabase(string username)
        {
            return CONTEXT.Users.ToList().Exists(u => u.UserName == username);
        }

        public bool ValidateUser(string username, string password)
        {
            return CONTEXT.Users.ToList().Exists(u => u.UserName == username && u.Password == password && u.IsDeleted == 0);
        }

        public bool DeleteUser(string username)
        {
            try
            {
                var user = CONTEXT.Users.FirstOrDefault(u => u.UserName == username);
                user.IsDeleted = 1;
                CONTEXT.Users.Update(user);
                CONTEXT.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
    }
}
