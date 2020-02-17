using DuaControl.Web.Data.Entities;
using System.Threading.Tasks;

namespace DuaControl.Web.Data.Helpers
{
    public interface IUserHelper
    {
        Task<User> GetUsersAsync(User user);

        Task<User> GetUserByUserNameAsync(string userName);

        Task<User> GetUserByIdAsync(int userId);

        Task<int> AddUserAsync(User user);

        Task UpdateUserAsync(User user);
    }
}