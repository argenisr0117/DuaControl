using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DuaControl.Web.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace DuaControl.Web.Data.Helpers
{
    public class RoleHelper : IRoleHelper
    {
        private readonly DataContext _dataContext;

        public RoleHelper(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<IList<Role>> GetAllRolesAsync()
        {
            var query = _dataContext.Roles;

            return await query.ToListAsync();
        }

        public async Task<IList<Role>> GetRolesForUserAsync(int userId)
        {
            var query = _dataContext.Roles
                .Where(r => r.UserRoles.Any(u => u.UserId == userId))
                .Select(r => r)
                .OrderBy(r => r.Name);

            return await query.ToListAsync();
        }

        public async Task<IList<UserRole>> GetUserRolesForUserAsync(int userId)
        {
            var query = _dataContext.UserRoles
                .Where(r => r.UserId == userId)
                .Select(r => r);

            return await query.ToListAsync();
        }

        //public async Task CheckRoleAsync(string roleName)
        //{
        //    var roleExists = _dataContext.Roles
        //        .FirstOrDefault(r => r.Name == roleName).ToString();

        //    if (string.IsNullOrEmpty(roleExists))
        //    {
        //        await _dataContext.Roles.AddAsync(new Role
        //        {
        //            Name = roleName
        //        });
        //        //await _roleManager.CreateAsync(new IdentityRole
        //        //{
        //        //    Name = roleName
        //        //});
        //    }
        //}
    }
}