using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DuaControl.Web.Data.Entities
{
    public class Factura
    {
        //Relationship between Factura and Adjuntos
        public ICollection<Adjunto> Adjuntos { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public Cliente Client { get; set; }
        public int Id { get; set; }

        [Display(Name = "Fecha Factura")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime InvoiceDate { get; set; }

        [Display(Name = "Fecha Factura")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd", ApplyFormatInEditMode = true)]
        public DateTime InvoiceDateLocal => InvoiceDate.ToLocalTime();

        [Display(Name = "# Factura")]
        [MaxLength(15, ErrorMessage = "El {0} campo no puede tener más de {1} caracteres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string InvoiceNumber { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public Puerto Port { get; set; } 

        [Display(Name = "Comentarios")]
        public string Remarks { get; set; }

        [Display(Name = "Detalle")]
        public string Details { get; set; }
    }
}
