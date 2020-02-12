using System;
using System.DirectoryServices.AccountManagement;
using System.Threading.Tasks;
using DuaControl.Web.Data;
using DuaControl.Web.Data.Entities;
using DuaControl.Web.Data.Helpers;
using DuaControl.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DuaControl.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly DataContext _dataContext;
        private readonly ICombosHelper _combosHelper;
        private readonly IUserHelper _userHelper;

        public AccountController(
            DataContext dataContext,
            ICombosHelper combosHelper,
            IUserHelper userHelper)
        {
            _dataContext = dataContext;
            _combosHelper = combosHelper;
            _userHelper = userHelper;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _dataContext.Users.ToListAsync());
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    using (PrincipalContext context = new PrincipalContext(ContextType.Domain))
                    {
                        bool login = context.ValidateCredentials(model.DomainUser, model.Password);
                        var usr = UserPrincipal.FindByIdentity(context, User.Identity.Name);
                        if (usr != null)
                        {
                            if (login)
                            {
                                return RedirectToAction("Index", "Facturas");
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error retrieving AD User", ex);
                }
            }

            ModelState.AddModelError(string.Empty, "Failed to login.");
            return View(model);
            //Cuando se devuelve el modelo, al usuario no se le borran los campos
            //del login que ya habia puesto
        }
        [Authorize(Roles = "Empleados")]
        public IActionResult AddUser()
        {
            var model = new AddUserViewModel
            {
                RolesList = _combosHelper.GetComboRoles()
            };

            return View(model);
        }
        [Authorize(Roles = "Empleados")]
        [HttpPost]
        public async Task<IActionResult> AddUser(AddUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    using (PrincipalContext context = new PrincipalContext(ContextType.Domain))
                    {
                        var usr = UserPrincipal.FindByIdentity(context, ContextType.Domain.ToString()+@"\"+model.DomainUser);
                        if (usr != null)
                        {
                            var user = new User
                            {
                                Email = usr.EmailAddress,
                                FullName = usr.DisplayName,
                                UserName = usr.SamAccountName,
                                DomainUser = usr.SamAccountName
                            };

                            var result = await _userHelper.AddUserAsync(user);
                            if (result != IdentityResult.Success)
                            {
                                return null;
                            }

                            await _userHelper.AddUserToRoleAsync(user, model.RoleId);
                            await _dataContext.SaveChangesAsync();
                            return RedirectToAction(nameof(Index));
                        }
                    }
                    return RedirectToAction("Facturas", "Index");

                }
                catch (Exception ex)
                {
                    throw new Exception("Error", ex);
                }
            }
            model.RolesList = _combosHelper.GetComboRoles();
            return View(model);

        }
        [AcceptVerbs("GET", "POST")]
        public async Task<IActionResult> VerifyUser(string DomainUser)
        {
            return Json(await _dataContext.Users.FirstOrDefaultAsync(p => p.DomainUser == DomainUser) == null ? (object)true : $"El usuario {DomainUser} ya existe.");
        }

        public IActionResult NotAuthorized()
        {
            return View();
        }

    }
}

