using System;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;

public class ClaimsTransformer : IClaimsTransformation
{
    public Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
    {
        var wi = (WindowsIdentity)principal.Identity;
        if (wi.Groups != null)
        {
            foreach (var group in wi.Groups) //-- Getting all the AD groups that user belongs to---  
            {
                try
                {
                    var claim = new Claim(wi.RoleClaimType, group.Value);
                    wi.AddClaim(claim);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        return Task.FromResult(principal);
    }
}

