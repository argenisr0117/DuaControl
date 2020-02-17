using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using DuaControl.Web.Data;
using DuaControl.Web.Data.Entities;
using DuaControl.Web.Data.Helpers;
using DuaControl.Web.Data.Ldap;
using DuaControl.Web.Security;
using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using IAuthenticationService = DuaControl.Web.Data.Ldap.IAuthenticationService;

namespace DuaControl.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddOptions();
            //services.Configure<CookiePolicyOptions>(options =>
            //{
            //    // This lambda determines whether user consent for non-essential cookies is needed for a given request.
            //    options.CheckConsentNeeded = context => true;
            //    options.MinimumSameSitePolicy = SameSiteMode.None;
            //});

            //services.ConfigureApplicationCookie(options =>
            //{
            //    options.LoginPath = "/Account/NotAuthorized";
            //    options.AccessDeniedPath = "/Account/NotAuthorized";
            //});

            //services.AddIdentity<User, IdentityRole>(cfg =>
            //{
            //    cfg.Password.RequireDigit = false;
            //    cfg.Password.RequiredUniqueChars = 0;
            //    cfg.Password.RequireLowercase = false;
            //    cfg.Password.RequireNonAlphanumeric = false;
            //    cfg.Password.RequireUppercase = false;
            //})
            //   .AddEntityFrameworkStores<DataContext>();
            //services.AddDistributedMemoryCache();

            //services.AddSession(options =>
            //{
            //    // Set a short timeout for easy testing.
            //    options.IdleTimeout = TimeSpan.FromSeconds(60);
            //    options.Cookie.HttpOnly = true;
            //    // Make the session cookie essential
            //    options.Cookie.IsEssential = true;
            //});
            services.AddDbContext<DataContext>(cfg =>
            {
                cfg.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            });
            services.AddAutoMapper();
            services.AddMemoryCache();
            services.AddSession();
            //services.AddAuthentication()
            //.AddCookie();

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
              .AddCookie(options =>
              {
                  options.AccessDeniedPath = "/Account/AccessDenied";
                  options.LoginPath = "/Account/Login";
              });

            // Set up policies from claims
            // https://leastprivilege.com/2016/08/21/why-does-my-authorize-attribute-not-work/
            services.AddAuthorization(options =>
            {
                options.AddPolicy(Constants.RoleNames.Administrator, policyBuilder =>
                {
                    policyBuilder.RequireAuthenticatedUser()
                        .RequireAssertion(context => context.User.HasClaim(ClaimTypes.Role, Constants.RoleNames.Administrator))
                        .Build();
                });
                options.AddPolicy(Constants.RoleNames.User, policyBuilder =>
                {
                    policyBuilder.RequireAuthenticatedUser()
                        .RequireAssertion(context => context.User.HasClaim(ClaimTypes.Role, Constants.RoleNames.User))
                        .Build();
                });
                options.AddPolicy(Constants.RoleNames.SuperUser, policyBuilder =>
                {
                    policyBuilder.RequireAuthenticatedUser()
                        .RequireAssertion(context => context.User.HasClaim(ClaimTypes.Role, Constants.RoleNames.SuperUser))
                        .Build();
                });
            });

            services.AddScoped<IUserHelper, UserHelper>();
            services.AddScoped<ICombosHelper, CombosHelper>();
            services.AddScoped<IConverterHelper, ConverterHelper>();
            services.AddTransient<SeedDb>(); //AddTrasient solo se ejecuta una vez
                                           //services.AddSingleton<IClaimsTransformation, ClaimsTransformer>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IRoleHelper, RoleHelper>();
            services.AddScoped<ISignInManager, SignInManager>();
            services.AddScoped<IUserSession, UserSession>();
            services.AddScoped<IUserHelper, UserHelper>();

            // Uncomment this to perform integration tests and UI tests in Development Environment.
            /*if (CurrentEnvironment.IsDevelopment())
            {
                services.AddScoped<IAuthenticationService, FakeAuthenticationService>();
            }
            else
            {*/
            services.AddScoped<IAuthenticationService, LdapAuthenticationService>();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Account/Error");
                app.UseStatusCodePagesWithRedirects("/Account/Error/{0}");
                app.UseHsts();
            }

            app.UseStatusCodePagesWithReExecute("/Account/error/{0}");
            app.UseAuthentication();
            app.UseStaticFiles();
            app.UseSession();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Account}/{action=Login}/{id?}");
            });

            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("es-DO");
        }
    }
}
