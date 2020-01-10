using DuaControl.Web.Data.Entities;
using DuaControl.Web.Models;
using System.Threading.Tasks;

namespace DuaControl.Web.Data.Helpers
{
    public interface IConverterHelper
    {
        FacturaViewModel ToFacturaViewModel(Factura factura);

       Task<Factura> ToFacturaAsync(FacturaViewModel factura);
    }
}
