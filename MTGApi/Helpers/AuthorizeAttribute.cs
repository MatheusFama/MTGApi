using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using MTGApi.Entities;

namespace MTGApi.Helpers
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        private readonly IList<Role> _roles;

        public AuthorizeAttribute(params Role[] roles)
        {
            _roles = roles ?? new Role[] {};
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var account = context.HttpContext.Items["Account"] as Account;

            //Verificando login
            if (account == null || (_roles.Any() && !_roles.Contains(account.Role)))
                context.Result = new JsonResult(new { message = "Não Autorizado" });

        }
    }
}
