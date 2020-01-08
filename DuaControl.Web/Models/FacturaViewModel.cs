using DuaControl.Web.Data.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DuaControl.Web.Models
{
    public class FacturaViewModel : Factura
    {
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [Display(Name = "Puerto")]
        [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar un puerto.")]
        public int PuertoId { get; set; }
        public IEnumerable<SelectListItem> Puertos { get; set; }

        [Display(Name = "Cliente")]
        public string Cliente { get; set; }
    }
}
