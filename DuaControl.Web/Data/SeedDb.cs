using DuaControl.Web.Data.Entities;
using DuaControl.Web.Data.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DuaControl.Web.Data
{
    public class SeedDb
    {
        private readonly DataContext _dataContext;
        private readonly IUserHelper _userHelper;

        public SeedDb(
            DataContext context,
            IUserHelper userHelper)
        {
            _dataContext = context;
            _userHelper = userHelper;
        }
        public async Task SeedAsync()
        {
            await _dataContext.Database.EnsureCreatedAsync();
            await CheckPortsAsync();
            await CheckRoles();
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
        private async Task CheckRoles()
        {
            await _userHelper.CheckRoleAsync("Admin");
            await _userHelper.CheckRoleAsync("SuperUser");
            await _userHelper.CheckRoleAsync("User");
        }
    }
}
