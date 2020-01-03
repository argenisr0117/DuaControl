using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DuaControl.Web.Data.Entities
{
    public class Cliente
    {
        [Display(Name = "Razón Social")]
        [MaxLength(100, ErrorMessage = "El {0} campo no puede tener más de {1} caracteres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        //[Remote(action: "VerifyPort", controller: "Ports")]
        public string BusinessName { get; set; }

        [Display(Name = "Código Cliente")]
        [MaxLength(10, ErrorMessage = "El {0} campo no puede tener más de {1} caracteres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [Key]
        //[Remote(action: "VerifyPort", controller: "Ports")]
        public string ClienteId { get; set; }

        ////Relationship between Facturas and Ports
        //public Factura Factura { get; set; }

        [Display(Name = "Cliente")]
        [MaxLength(100, ErrorMessage = "El {0} campo no puede tener más de {1} caracteres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        //[Remote(action: "VerifyPort", controller: "Ports")]
        public string Name { get; set; }
    }
}