using DuaControl.Web.Data.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DuaControl.Web.Models
{
    public class AddUserViewModel : User
    {
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public int RoleId { set; get; }
        public IEnumerable<SelectListItem> RolesList { get; set; }
    }
}
