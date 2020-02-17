namespace DuaControl.Web.Data.Ldap
{
    public interface IAuthenticationService
    {
        bool ValidateUser(string domain, string userName, string password);
    }

}
   
