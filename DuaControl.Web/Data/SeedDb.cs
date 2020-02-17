using DuaControl.Web.Data.Entities;
using DuaControl.Web.Data.Helpers;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace DuaControl.Web.Data
{
    public class SeedDb
    {
        private readonly DataContext _dataContext;
        private readonly IRoleHelper _roleHelper;

        public SeedDb(
            DataContext context,
            IRoleHelper roleHelper)
        {
            _dataContext = context;
            _roleHelper = roleHelper;
        }

        public async Task SeedAsync()
        {
            await _dataContext.Database.EnsureCreatedAsync();
            await CheckPortsAsync();
            await CheckRolesAsync();
            await CheckUserAsync();
            //await CheckAgendasAsync();
        }

        private async Task CheckPortsAsync()
        {
            if (!_dataContext.Puertos.Any())
            {
                _dataContext.Puertos.Add(new Puerto { Name = "Default" });
                _dataContext.Puertos.Add(new Puerto { Name = "Elias Piña" });
                _dataContext.Puertos.Add(new Puerto { Name = "Dajabón" });
                _dataContext.Puertos.Add(new Puerto { Name = "Venezuela" });
                _dataContext.Puertos.Add(new Puerto { Name = "Puerto Rico" });
                await _dataContext.SaveChangesAsync();
            }
        }

        private async Task CheckRolesAsync()
        {
            if (!_dataContext.Roles.Any())
            {
                _dataContext.Roles.Add(new Role { Name = "Admin" });
                _dataContext.Roles.Add(new Role { Name = "SuperUser" });
                _dataContext.Roles.Add(new Role { Name = "User" });
                await _dataContext.SaveChangesAsync();
            }
            //await _roleHelper.CheckRoleAsync("Admin");
            //await _roleHelper.CheckRoleAsync("SuperUser");
            //await _roleHelper.CheckRoleAsync("User");
        }

        private async Task CheckUserAsync()
        {
            if (!_dataContext.Users.Any())
            {
                _dataContext.Users.Add(new User { UserName = "soportet", FirstName = "Soporte", LastName = "Técnico", IsActive = true, LastLoginDate = DateTime.Now, CreatedOn = DateTime.Now, CreatedBy = "soportet", ModifiedOn = DateTime.Now, ModifiedBy = "soportet" });
                await _dataContext.SaveChangesAsync();
                var user = _dataContext.Users.FirstOrDefault();
                var role = _dataContext.Roles.FirstOrDefault(r => r.Name == "Admin");
                _dataContext.UserRoles.Add(new UserRole { Role = role, User = user });
                await _dataContext.SaveChangesAsync();
            }
            //await _roleHelper.CheckRoleAsync("Admin");
            //await _roleHelper.CheckRoleAsync("SuperUser");
            //await _roleHelper.CheckRoleAsync("User");
        }
    }
}