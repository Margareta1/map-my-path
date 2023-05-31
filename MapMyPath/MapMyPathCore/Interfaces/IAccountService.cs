using MapMyPathLib;

namespace MapMyPathCore.Interfaces
{
    public interface IAccountService
    {
        IList<AppUser> GetUsers();

        bool ExistsInDatabase(string username);

        bool AddUser(AppUser user);

        bool ValidateUser(string username, string password);

        bool DeleteUser(string username);
    }
}