using System;
using System.Collections.Generic;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.Threading.Tasks;
using DuaControl.Web.Data;
using DuaControl.Web.Data.Entities;
using DuaControl.Web.Data.Helpers;
using DuaControl.Web.Data.Ldap;
using DuaControl.Web.Models;
using DuaControl.Web.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DuaControl.Web.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly IAuthenticationService _authenticationService;
        //private readonly ILogger<AccountController> _logger;
        private readonly IRoleHelper _roleHelper;
        private readonly ISignInManager _signInManager;
        private readonly IUserHelper _userHelper;
        private readonly DataContext _dataContext;
        private readonly ICombosHelper _combosHelper;
        private readonly IUserSession _userSession;

        public AccountController(
            IAuthenticationService authenticationService,
            //ILogger<AccountController> logger,
            IRoleHelper roleHelper,
            ISignInManager signInManager,
            IUserHelper userHelper,
            DataContext dataContext,
            ICombosHelper combosHelper,
            IUserSession userSession)
        {
            _authenticationService = authenticationService;
            //_logger = logger;
            _roleHelper = roleHelper;
            _signInManager = signInManager;
            _userHelper = userHelper;
            _dataContext = dataContext;
            _combosHelper = combosHelper;
            _userSession = userSession;
        }

        //
        // GET: /Account/Login
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Login(string returnUrl = null)
        {
            // Clear the existing external cookie to ensure a clean login process
            await _signInManager.SignOutAsync();

            ViewData["ReturnUrl"] = returnUrl;
            //var model = new LoginViewModel { AvailableDomains = await GetDomains() };
            return View("Login");
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid)
            {
                bool result = _authenticationService.ValidateUser("lafabril.com.do", model.DomainUser, model.Password);
                if (result)
                {
                    var user = await _userHelper.GetUserByUserNameAsync(model.DomainUser);
                    if (user != null && user.IsActive)
                    {
                        var roleNames = (await _roleHelper.GetRolesForUserAsync(user.Id)).Select(r => r.Name).ToList();
                        await _signInManager.SignInAsync(user, roleNames);

                        user.LastLoginDate = DateTime.Now;
                        await _userHelper.UpdateUserAsync(user);

                        //_logger.LogInformation($"Login Successful: {user.UserName}.");

                        if (!string.IsNullOrEmpty(returnUrl) && !string.Equals(returnUrl, "/") && Url.IsLocalUrl(returnUrl))
                            return RedirectToLocal(returnUrl);

                        //if (roleNames.Contains(Constants.RoleNames.Administrator))
                        //    return RedirectToAction(nameof(DashboardController.Index), "Dashboard", new { area = Constants.Areas.Administration });

                        return RedirectToAction("Index", "Facturas");
                    }
                    //_logger.LogError($"Authorization Fail: {model.UserName}");
                    ModelState.AddModelError("", Constants.Messages.AccessDenied);
                }
                else
                {
                    //_logger.LogError($"Login Fail: {model.UserName} - Incorrect username or password. ");
                    ModelState.AddModelError("", "Incorrect username or password.");
                }
            }

            // If we got this far, something failed, redisplay form
            //model.AvailableDomains = await GetDomains();
            return View("Login", model);
        }

        //
        // POST: /Account/Logout
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            //_logger.LogInformation($"Logout Successful: {User.Identity.Name}");
            return RedirectToAction(nameof(AccountController.Login));
        }


        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }
        public IActionResult Error(string id = "")
        {
            switch (id)
            {
                case "403":
                    return View("AccessDenied");
                case "404":
                    return View("PageNotFound");
                default:
                    return View();
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult AccessDenied()
        {
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult PageNotFound()
        {
            Response.StatusCode = 404;

            return View();
        }

        [Authorize(Policy = Constants.RoleNames.Administrator)]
        public async Task<IActionResult> Index()
        {
            return View(await _dataContext.Users.ToListAsync());
        }

        [Authorize(Policy = Constants.RoleNames.Administrator)]
        public IActionResult AddUser()
        {
            var model = new AddUserViewModel
            {
                RolesList = _combosHelper.GetComboRoles()
            };

            return View(model);
        }

        [HttpPost]
        [Authorize(Policy = Constants.RoleNames.Administrator)]
        public async Task<IActionResult> AddUser(AddUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    using (PrincipalContext context = new PrincipalContext(ContextType.Domain))
                    {
                        var usr = UserPrincipal.FindByIdentity(context, ContextType.Domain.ToString() + @"\" + model.UserName);
                        if (usr != null)
                        {
                            var user = new User
                            {
                                ModifiedOn = DateTime.Now,
                                LastLoginDate = DateTime.Now,
                                IsActive = true,
                                CreatedOn = DateTime.Now,
                                UserName = usr.SamAccountName,
                                FirstName=usr.GivenName,
                                LastName=usr.Surname,
                                CreatedBy=_userSession.UserName,
                                ModifiedBy=_userSession.UserName,
                            };

                            var result = await _userHelper.AddUserAsync(user);
                            if (result <0)
                            {
                                return null;
                            }

                            _dataContext.UserRoles.Add(new UserRole { RoleId = model.RoleId, UserId = result });
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
            return Json(await _dataContext.Users.FirstOrDefaultAsync(p => p.UserName == DomainUser) == null ? (object)true : $"El usuario {DomainUser} ya existe.");
        }
    }

    //public class AccountController : Controller
    //{
    //    private readonly DataContext _dataContext;
    //    private readonly ICombosHelper _combosHelper;
    //    private readonly IUserHelper _userHelper;

    //    public AccountController(
    //        DataContext dataContext,
    //        ICombosHelper combosHelper,
    //        IUserHelper userHelper)
    //    {
    //        _dataContext = dataContext;
    //        _combosHelper = combosHelper;
    //        _userHelper = userHelper;
    //    }

    //    public async Task<IActionResult> Index()
    //    {
    //        return View(await _dataContext.Users.ToListAsync());
    //    }
    //    public IActionResult Login()
    //    {
    //        return View();
    //    }
    //    [HttpPost]
    //    public IActionResult Login(LoginViewModel model)
    //    {
    //        if (ModelState.IsValid)
    //        {
    //            try
    //            {
    //                using (PrincipalContext context = new PrincipalContext(ContextType.Domain))
    //                {
    //                    bool login = context.ValidateCredentials(model.DomainUser, model.Password);
    //                    var usr = UserPrincipal.FindByIdentity(context, User.Identity.Name);
    //                    if (usr != null)
    //                    {
    //                        if (login)
    //                        {
    //                            return RedirectToAction("Index", "Facturas");
    //                        }
    //                    }
    //                }
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
    //    [Authorize(Roles = "Empleados")]
    //    public IActionResult AddUser()
    //    {
    //        var model = new AddUserViewModel
    //        {
    //            RolesList = _combosHelper.GetComboRoles()
    //        };

    //        return View(model);
    //    }
    //    [Authorize(Roles = "Empleados")]
    //    [HttpPost]
    //    public async Task<IActionResult> AddUser(AddUserViewModel model)
    //    {
    //        if (ModelState.IsValid)
    //        {
    //            try
    //            {
    //                using (PrincipalContext context = new PrincipalContext(ContextType.Domain))
    //                {
    //                    var usr = UserPrincipal.FindByIdentity(context, ContextType.Domain.ToString()+@"\"+model.DomainUser);
    //                    if (usr != null)
    //                    {
    //                        var user = new User
    //                        {
    //                            Email = usr.EmailAddress,
    //                            FullName = usr.DisplayName,
    //                            UserName = usr.SamAccountName,
    //                            DomainUser = usr.SamAccountName
    //                        };

    //                        var result = await _userHelper.AddUserAsync(user);
    //                        if (result != IdentityResult.Success)
    //                        {
    //                            return null;
    //                        }

    //                        await _userHelper.AddUserToRoleAsync(user, model.RoleId);
    //                        await _dataContext.SaveChangesAsync();
    //                        return RedirectToAction(nameof(Index));
    //                    }
    //                }
    //                return RedirectToAction("Facturas", "Index");

    //            }
    //            catch (Exception ex)
    //            {
    //                throw new Exception("Error", ex);
    //            }
    //        }
    //        model.RolesList = _combosHelper.GetComboRoles();
    //        return View(model);

    //    }
    //    [AcceptVerbs("GET", "POST")]
    //    public async Task<IActionResult> VerifyUser(string DomainUser)
    //    {
    //        return Json(await _dataContext.Users.FirstOrDefaultAsync(p => p.DomainUser == DomainUser) == null ? (object)true : $"El usuario {DomainUser} ya existe.");
    //    }

    //    public IActionResult NotAuthorized()
    //    {
    //        return View();
    //    }

    //}
}

