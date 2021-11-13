using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;

namespace EnergiaElectrica.ui.Filters
{
    public class ValidarAutenticacion : System.Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext filter)
        {
            
            if (filter.HttpContext.Session.Get("USUARIO") == null)
            {
                filter.Result = new RedirectToRouteResult(
                    new RouteValueDictionary{{ "controller", "Login" },
                        { "action", "Index" }
                    }); ;
            }
        }
    }
}
