using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;
using SpotifyProxyAPI.Models;

namespace SpotifyProxyAPI.Helpers
{
    /// <summary>
    /// Validating the request 
    /// </summary>
    public class ModelValidationFilterAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// Method which throws a custom error response if model is invalid
        /// </summary>
        /// <param name="context"></param>
        public override void OnActionExecuting(ActionExecutingContext context)
        {

            if (!context.ModelState.IsValid)
            {
                var response = new ErrorResponse
                {
                    StatusCode = 400,
                    ErrorMessage = string.Join(", ", context.ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage))
                };

                context.Result = new BadRequestObjectResult(response);

            }
        }
    }
}
