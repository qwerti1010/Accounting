using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WebAccounting;

public class FeatureEnabledAttribute : Attribute, IResourceFilter
{
    public bool IsEnabled { get; set; }
    public void OnResourceExecuted(ResourceExecutedContext context)
    {
        if (!IsEnabled)
        {
            context.Result = new BadRequestResult();            
        }       
    }

    public void OnResourceExecuting(ResourceExecutingContext context)
    {
    }
}
