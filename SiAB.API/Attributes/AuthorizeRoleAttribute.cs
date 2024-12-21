using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using SiAB.Core.Enums;

namespace SiAB.API.Attributes
{
    public class AuthorizeRoleAttribute : AuthorizeAttribute, IAuthorizationFilter    
    { 
        private readonly UsuarioRole[] _roles;
        public AuthorizeRoleAttribute(params UsuarioRole[] roles) 
        {
            _roles = roles;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            
            var user = context.HttpContext.User;

            if (!user.Identity.IsAuthenticated)
            {
                context.Result = new Microsoft.AspNetCore.Mvc.UnauthorizedResult();
                return;
            }

            var userRoles = user.Claims
                .Where(c => c.Type == "role")
                .Select(c => Enum.Parse<UsuarioRole>(c.Value));

            if (!_roles.Any(r => userRoles.Contains(r)))
            {
                context.Result = new Microsoft.AspNetCore.Mvc.ForbidResult();
                return;
            }
        }
    }
}