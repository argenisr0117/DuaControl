using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DuaControl.Web.Data.Entities
{
    public class Puerto
    {
        //Relationship between Facturas and Ports
        public ICollection<Factura> Facturas { get; set; }

        public int Id { get; set; }

        [Display(Name = "Puerto")]
        [MaxLength(50, ErrorMessage = "El {0} campo no puede tener más de {1} caracteres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [Remote(action: "VerifyPort", controller: "Puertos")]
        //[Remote(action: "VerifyPort", controller: "Ports")]
        public string Name { get; set; }
    }
}