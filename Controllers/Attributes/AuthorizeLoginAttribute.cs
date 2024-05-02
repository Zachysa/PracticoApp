using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Practico.Controllers.Attributes
{
    [AttributeUsage(AttributeTargets.All, Inherited = false)]
    public class LoginAuthAttribute : Attribute, IFilterMetadata, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            string? value = context.HttpContext.Session.GetString("IdRole");
            if (value == null || value == "")
            {
                context.Result = new RedirectToActionResult("login", "Home", null);
            }
        }
    }

}
