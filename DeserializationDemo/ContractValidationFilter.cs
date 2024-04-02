using Microsoft.AspNetCore.Mvc.Filters;

namespace DeserializationDemo;

internal sealed class ContractValidationFilter : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext actionContext)
    {
        if (actionContext == null)
        {
            throw new ArgumentNullException(nameof(actionContext));
        }

        if (!actionContext.ModelState.IsValid)
        {
            throw new Exception("Contract validation failed");
        }
    }
}