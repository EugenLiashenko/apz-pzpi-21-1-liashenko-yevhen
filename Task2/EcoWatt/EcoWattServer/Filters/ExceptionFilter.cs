using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace EcoWattServer.Filters;

public class ExceptionFilter: IExceptionFilter
{
    private readonly IHostEnvironment _hostEnvironment;

    public ExceptionFilter(IHostEnvironment hostEnvironment) =>
        _hostEnvironment = hostEnvironment;

    public void OnException(ExceptionContext context)
    {
        if (context is null)
        {
            throw new ArgumentNullException("context");
        }

        if (!_hostEnvironment.IsDevelopment())
        {
            return;
        }

        context.Result = new ContentResult
        {
            Content = context.Exception.ToString()
        };
    }
}