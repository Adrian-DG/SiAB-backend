using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using SiAB.Core.Enums;

namespace SiAB.API.Attributes
{
    public class AuthorizeRoleAttribute : AuthorizeAttribute, IAuthorizationFilter    
    { 
        private readonly string[] _roles;
        public AuthorizeRoleAttribute(params UsuarioRole[] roles) 
        {            
            _roles = roles.Select(r => r.ToString()).ToArray();
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            
            var user = context.HttpContext.User;

            if (!user.Identity.IsAuthenticated)
            {
                context.Result = new Microsoft.AspNetCore.Mvc.UnauthorizedResult();
                return;
            }

			if (!_roles.Any(r => user.IsInRole(r)))
            {
                context.Result = new Microsoft.AspNetCore.Mvc.ForbidResult();
                return;
            }
        }
    }
}