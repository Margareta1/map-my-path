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
            var users = CONTEXT.Users.Where(u=> u.IsDeleted==0).ToList();
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
                    Email = item.Email

                });
            }
            return modelUsers;
        }

        public AppUser GetUserById(Guid id)
        {
            var user = CONTEXT.Users.First(x => x.Id == id.ToString());
            return new AppUser
            {
                Id = id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                CreatedAt = user.CreatedAt,
                IsDeleted = user.IsDeleted,
                Password = user.Password,
                UserName = user.UserName,
                Email = user.Email
            };
        }

        public bool AddUser(AppUser user)
        {
            try
            {
                CONTEXT.Users.Add(new Areas.Identity.Data.MapMyPathCoreUser
                {
                    CreatedAt = user.CreatedAt,
                    Email = user.Email,
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
    }
}
