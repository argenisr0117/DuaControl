using DuaControl.Web.Data.Entities;
using DuaControl.Web.Models;
using System.Threading.Tasks;

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

        public async Task<Factura>  ToFacturaAsync(FacturaViewModel factura)
        {
            var Factura = new Factura
            {
                Id = factura.Id,
                InvoiceNumber=factura.InvoiceNumber,
                InvoiceDate=factura.InvoiceDate,
                InvoiceSystem=factura.InvoiceSystem,
                InvoiceUser=factura.InvoiceUser,
                Details=factura.Details,
                Client=await _dataContext.Clientes.FindAsync(factura.ClienteId),
                Port = await _dataContext.Puertos.FindAsync(factura.PuertoId),
                Remarks = factura.Remarks
            };

            return Factura;
        }

        public FacturaViewModel ToFacturaViewModel(Factura factura)
        {
            return new FacturaViewModel
            {
                InvoiceDate = factura.InvoiceDate,
                InvoiceNumber = factura.InvoiceNumber,
                InvoiceSystem = factura.InvoiceSystem,
                Client =factura.Client,
                ClienteId=factura.Client.ClienteId,
                Port = factura.Port,
                Adjuntos=factura.Adjuntos,
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