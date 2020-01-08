using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace DuaControl.Web.Data.Helpers
{
    public interface ICombosHelper
    {
        IEnumerable<SelectListItem> GetComboPorts();
    }
}
