using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace DuaControl.Web.Data.Entities
{
    public class User : IdentityUser
    {
        [Display(Name = "Usuario")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [MaxLength(15, ErrorMessage ="El campo {0} no puede tener más de {1} caracteres.")]
        [Remote(action: "VerifyUser", controller: "Account")]
        public string DomainUser { get; set; }

        //[Display(Name = "Nombre")]
        //[MaxLength(50, ErrorMessage = "El campo {0} no puede tener más de {1} caracteres.")]
        //[Required(ErrorMessage = "El campo {0} es obligatorio.")]
        //public string FullName { get; set; }
    }
}
