using DuaControl.Web.Data.Ldap;
using Novell.Directory.Ldap;

namespace DuaControl.Web.Data.Ldap
{
    /// <summary>
    /// As of today, System.DirectoryServices hasn't been implemented in ASP.NET Core yet,
    /// so we need to use Novell.Directory.Ldap.NETStandard.
    /// https://github.com/dotnet/corefx/issues/2089
    /// https://github.com/dsbenghe/Novell.Directory.Ldap.NETStandard
    /// </summary>
    public class LdapAuthenticationService : IAuthenticationService
    {
        public bool ValidateUser(string domainName, string username, string password)
        {
            string userDn = $"{username}@{domainName}";
            try
            {
                using (var connection = new LdapConnection { SecureSocketLayer = false })
                {
                    connection.Connect(domainName, 389);
                    connection.Bind(userDn, password);

                    if (connection.Bound)
                        return true;
                }
            }
            catch (LdapException ex)
            {
                // Log exception
            }
            return false;
        }
    }
}