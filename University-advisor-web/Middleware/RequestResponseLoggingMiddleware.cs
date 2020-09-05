using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using University_advisor_web.Interfaces;

namespace University_advisor_web.Middleware
{
    public class RequestResponseLoggingMiddleware : TypeFilterAttribute
    {
        public RequestResponseLoggingMiddleware() : base(typeof(AutoLogActionFilterImpl))
        {

        }

        private class AutoLogActionFilterImpl : IActionFilter
        {
            private readonly ILogger _logger;
            public AutoLogActionFilterImpl(ILogger logger)
            {
                _logger = logger;
            }

            public void OnActionExecuting(ActionExecutingContext context)
            {
                // perform some business logic work
                //_logger.Log("context.HttpContext.Request", "HTTP");
            }

            public void OnActionExecuted(ActionExecutedContext context)
            {
                //TODO: log body content and response as well
                _logger.Log($"path: {context.HttpContext.Request.Path}", "HTTP");
            }
        }
    }
}
