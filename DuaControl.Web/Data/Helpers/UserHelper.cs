using DuaControl.Web.Data.Entities;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace DuaControl.Web.Data.Helpers
{
    public class UserHelper : IUserHelper
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;

        public UserHelper(
            UserManager<User> userManager,
            RoleManager<IdentityRole> roleManager,
            SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
        }

        public async Task<IdentityResult> AddUserAsync(User user)
        {
            return await _userManager.CreateAsync(user);
        }

        public async Task AddUserToRoleAsync(User user, string roleName)
        {
            await _userManager.AddToRoleAsync(user, roleName);
        }

        public async Task CheckRoleAsync(string roleName)
        {
            var roleExists = await _roleManager.RoleExistsAsync(roleName);
            if (!roleExists)
            {
                await _roleManager.CreateAsync(new IdentityRole
                {
                    Name = roleName
                });
            }
        }
    }
}
