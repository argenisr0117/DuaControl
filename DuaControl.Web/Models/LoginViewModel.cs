using System.ComponentModel.DataAnnotations;

namespace DuaControl.Web.Models
{
    public class LoginViewModel
    {
        [Required]
        public string DomainUser { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
