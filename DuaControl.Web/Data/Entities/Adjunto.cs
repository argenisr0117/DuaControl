using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DuaControl.Web.Data.Entities
{
    public class Adjunto
    {
        public int Id { get; set; }

        [Display(Name = "Fecha Registro")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd hh:mm tt}", ApplyFormatInEditMode = true)]
        public DateTime RegisterDate { get; set; }

        //[Display(Name = "Fecha Registro")]
        //[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd hh:mm tt}", ApplyFormatInEditMode = true)]
        //public DateTime RegisterDateLocal => RegisterDate.ToLocalTime();

        [Column(TypeName = "VARCHAR(20)")]
        [MaxLength(20)]
        public string User { get; set; }

        [Column(TypeName = "VARCHAR(120)")]
        [MaxLength(120)]
        public string DocumentName { get; set; }

        public Factura Factura { get; set; }

        public string DocumentUrl { get; set; }

    }
}