using DuaControl.Web.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DuaControl.Web.Data
{
    public class SeedDb
    {
        private readonly DataContext _dataContext;

        public SeedDb(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task SeedAsync()
        {
            await _dataContext.Database.EnsureCreatedAsync();
            await CheckPortsAsync();
            //await CheckAgendasAsync();
        }

        private async Task CheckPortsAsync()
        {
            if (!_dataContext.Puertos.Any())
            {
                _dataContext.Puertos.Add(new Puerto { Name = "Default" });
                _dataContext.Puertos.Add(new Puerto { Name = "Elias Piña" });
                _dataContext.Puertos.Add(new Puerto { Name = "Dajabón" });
                await _dataContext.SaveChangesAsync();
            }
        }
    }
}
