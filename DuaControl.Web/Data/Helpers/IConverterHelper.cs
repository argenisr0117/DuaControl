using DuaControl.Web.Data.Entities;
using DuaControl.Web.Models;

namespace DuaControl.Web.Data.Helpers
{
    public interface IConverterHelper
    {
        FacturaViewModel ToFacturaViewModel(Factura factura);
    }
}
