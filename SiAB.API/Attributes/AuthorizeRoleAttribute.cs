using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using SiAB.Core.Enums;

namespace SiAB.API.Attributes
{
    public class AuthorizeRoleAttribute : AuthorizeAttribute, IAuthorizationFilter    
    { 
        private readonly string[] _roles;
        public AuthorizeRoleAttribute(params UsuarioRolesEnum[] roles) 
        {            
            _roles = roles.Select(r => r.ToString().Replace("_"," ")).ToArray();
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            
            var user = context.HttpContext.User;

            if (!user.Identity.IsAuthenticated)
            {
                context.Result = new Microsoft.AspNetCore.Mvc.UnauthorizedResult();
                return;
            }

            var roles = user.Claims.Where(c => c.Type == "Roles").Select(c => c.Value).ToList();

			// Debugging: Log or inspect the roles
			Console.WriteLine("User roles: " + string.Join(", ", roles));
			Console.WriteLine("Required roles: " + string.Join(", ", _roles));

			if (!_roles.Any(r => roles.Contains(r)))
            {
                context.Result = new Microsoft.AspNetCore.Mvc.ForbidResult();
                return;
            }
        }
    }
}