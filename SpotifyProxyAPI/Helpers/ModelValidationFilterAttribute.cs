using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;
using SpotifyProxyAPI.Models;

namespace SpotifyProxyAPI.Helpers
{
    public class ModelValidationFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext actionContext)
        {

            if (!actionContext.ModelState.IsValid)
            {
                var response = new Error
                {
                    StatusCode = 400,
                    ErrorMessage = string.Join(", ", actionContext.ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage))
                };

                actionContext.Result = new BadRequestObjectResult(response);

            }
        }
    }
}
