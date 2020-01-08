using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;

namespace DuaControl.Web.Data.Helpers
{
    public class CombosHelper :ICombosHelper
    {
        private readonly DataContext _dataContext;

        public CombosHelper(DataContext dataContext)
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
    }
}
