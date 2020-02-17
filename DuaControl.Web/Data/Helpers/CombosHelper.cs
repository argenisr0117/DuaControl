using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;

namespace DuaControl.Web.Data.Helpers
{
    public class CombosHelper :ICombosHelper
    {
        private readonly DataContext _dataContext;
        //private readonly RoleHelper _roleHelper;

        public CombosHelper(
            DataContext dataContext
           )
        {
            _dataContext = dataContext;
          
        }
        public IEnumerable<SelectListItem> GetComboPorts()
        {
            var list = _dataContext.Puertos.Select(port => new SelectListItem
            {
                Text = port.Name,
                Value = $"{port.Id}"
            })
               .OrderBy(port => port.Text)
               .ToList();

            list.Insert(0, new SelectListItem
            {
                Text = "[Seleccione un puerto]",
                Value = "0"
            });
            return list;
        }

        public IEnumerable<SelectListItem> GetComboRoles()
        {
            var list = _dataContext.Roles.Select(role => new SelectListItem
            {
                Text = role.Name,
                Value = $"{role.Id}"
            })
               .OrderBy(port => port.Text)
               .ToList();

            list.Insert(0, new SelectListItem
            {
                Text = "[Seleccione un rol]",
                Value = "0"
            });
            return list;
        }
    }
}
