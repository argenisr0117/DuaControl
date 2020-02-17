using DuaControl.Web.Data.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DuaControl.Web.Data.Helpers
{
    public interface IRoleHelper
    {
        Task<IList<Role>> GetAllRolesAsync();

        Task<IList<Role>> GetRolesForUserAsync(int userId);

        Task<IList<UserRole>> GetUserRolesForUserAsync(int userId);

        //Task CheckRoleAsync(string roleName);
    }
}