using DuaControl.Web.Data.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DuaControl.Web.Security
{
    public interface ISignInManager
    {
        Task SignInAsync(User user, IList<string> roleNames);

        Task SignOutAsync();
    }
}