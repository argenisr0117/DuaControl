using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DuaControl.Web.Data;
using DuaControl.Web.Data.Helpers;
using DuaControl.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace DuaControl.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly DataContext _dataContext;
        private readonly CombosHelper _combosHelper;

        public AccountController(
            DataContext dataContext,
            CombosHelper combosHelper)
        {
            _dataContext = dataContext;
            _combosHelper = combosHelper;
        }
        public IActionResult Index()
        {
            return View();
        }

    //    [HttpPost]
    //    public IActionResult Login(LoginViewModel model)
    //    {
    //        if (ModelState.IsValid)
    //        {
    //            try
    //            {
    //                //PrincipalContext context = new PrincipalContext(ContextType.Domain);
    //                //UserPrincipal principal = new UserPrincipal(context);

    //                //if (context != null)
    //                //{
    //                //    principal = UserPrincipal.FindByIdentity(context, IdentityType.SamAccountName, identity.Name);
    //                //}

    //                using (PrincipalContext context = new PrincipalContext(ContextType.Domain))
    //                {
    //                    bool login = context.ValidateCredentials(model.Usuario, model.Password);
    //                    var usr = UserPrincipal.FindByIdentity(context, User.Identity.Name);
    //                    if (usr != null)
    //                    {
    //                        if (login)
    //                        {
    //                            return RedirectToAction("Index", "Home");
    //                        }
    //                    }
    //                }
    //                // return AdUser.CastToAdUser(principal);
    //            }
    //            catch (Exception ex)
    //            {
    //                throw new Exception("Error retrieving AD User", ex);
    //            }
    //        }

    //        ModelState.AddModelError(string.Empty, "Failed to login.");
    //        return View(model);
    //        //Cuando se devuelve el modelo, al usuario no se le borran los campos
    //        //del login que ya habia puesto
    //    }

    //}
}
