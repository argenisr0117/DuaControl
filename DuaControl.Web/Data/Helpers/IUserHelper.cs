using DuaControl.Web.Data.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DuaControl.Web.Data.Helpers
{
    public interface IUserHelper
    {
        Task CheckRoleAsync(string roleName);
        Task<IdentityResult> AddUserAsync(User user);
        Task AddUserToRoleAsync(User user, string roleName);
    }
}
