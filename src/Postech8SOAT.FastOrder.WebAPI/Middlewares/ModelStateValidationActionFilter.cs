using Microsoft.AspNetCore.Mvc.Filters;
using Postech8SOAT.FastOrder.Domain.Exceptions;
using System.Text.Json;

namespace Postech8SOAT.FastOrder.WebAPI.Middlewares
{
    public class ModelStateValidationActionFilter : ActionFilterAttribute
    {
    
        public override Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (context.ModelState.IsValid is false)
            {
                throw new DomainExceptionValidation(JsonSerializer.Serialize(context.ModelState));
            }

            return next();
        }
    }
}
