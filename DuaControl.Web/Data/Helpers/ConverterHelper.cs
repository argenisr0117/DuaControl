using DuaControl.Web.Data.Entities;
using DuaControl.Web.Models;

namespace DuaControl.Web.Data.Helpers
{
    public class ConverterHelper : IConverterHelper
    {
        private readonly ICombosHelper _combosHelper;
        private readonly DataContext _dataContext;

        public ConverterHelper(
            DataContext dataContext,
            ICombosHelper combosHelper)
        {
            _dataContext = dataContext;
            _combosHelper = combosHelper;
        }

        public FacturaViewModel ToFacturaViewModel(Factura factura)
        {
            return new FacturaViewModel
            {
                InvoiceDate = factura.InvoiceDate,
                InvoiceNumber = factura.InvoiceNumber,
                InvoiceSystem = factura.InvoiceSystem,
                //Id = model.Id == 0 ? null :model.Id,
                InvoiceUser = factura.InvoiceUser,
                Cliente = factura.Client.Name,
                Details = factura.Details,
                Remarks = factura.Remarks,
                Id = factura.Id,
                PuertoId = factura.Port.Id,
                Puertos = _combosHelper.GetComboPorts()
            };
        }
    }
}