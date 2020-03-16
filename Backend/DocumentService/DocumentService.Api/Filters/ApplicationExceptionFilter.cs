using DocumentService.Api.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace DocumentService.Api.Filters
{
    public class ApplicationExceptionFilter : ExceptionFilterAttribute
    {
        private readonly ILogger _logger;

        public ApplicationExceptionFilter(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger(typeof(ApplicationExceptionFilter).Name);
        }

        public override void OnException(ExceptionContext context)
        {
            _logger.LogError(
                $"Request throwed {context.Exception}");
            
            context.Result = new JsonResult(new Message
            {
                IsSuccess = false,
                ErrorMessage = context.Exception.Message
            });
        }
    }
}