using DuaControl.Web.Data.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DuaControl.Web.Models
{
    public class FacturaViewModel : Factura
    {
        [Display(Name = "Cliente")]
        public string Cliente { get; set; }

        public string ClienteId { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [Display(Name = "Puerto")]
        [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar un puerto.")]
        public int PuertoId { get; set; }
        public IEnumerable<SelectListItem> Puertos { get; set; }
    }
}