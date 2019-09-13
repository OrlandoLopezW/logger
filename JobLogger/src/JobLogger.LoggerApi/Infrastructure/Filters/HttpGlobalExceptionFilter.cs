using JobLogger.LoggerApi.Infrastructure.Exceptions;
using JobLogger.LoggerApi.Infrastructure.Utility.Constants;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;
using System.Net;

namespace JobLogger.LoggerApi.Infrastructure.Filters
{
    public class HttpGlobalExceptionFilter : IExceptionFilter
    {
        private readonly IHostingEnvironment env;

        public HttpGlobalExceptionFilter(IHostingEnvironment env)
        {
            this.env = env;
        }

        public void OnException(ExceptionContext context)
        {
            if (context.Exception.GetType() == typeof(LoggerApiException))
            {
                JsonErrorResponse json = null;
                var innerException = context.Exception.InnerException;

                if (innerException == null)
                {
                    json = new JsonErrorResponse
                    {
                        Messages = new string[] { context.Exception.Message },
                        MessageType = NotificationMessageType.BUSINESSLOGIC
                    };
                }
                else
                {
                    var errors = ((FluentValidation.ValidationException)innerException).Errors.Select(x => x.ErrorMessage).ToArray();
                    json = new JsonErrorResponse
                    {
                        Messages = errors,
                        MessageType = NotificationMessageType.FORMFIELDS
                    };
                }
                context.Result = new BadRequestObjectResult(json);
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            }
            else
            {
                var json = new JsonErrorResponse
                {
                    Messages = new[] { "Ocurrió un error interno, intente nuevamente por favor.", context.Exception.Message },
                    MessageType = NotificationMessageType.INTERNALERROR
                };

                context.Result = new InternalServerErrorObjectResult(json);
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            }
            context.ExceptionHandled = true;
        }
    }

    public class InternalServerErrorObjectResult : ObjectResult
    {
        public InternalServerErrorObjectResult(object error)
            : base(error)
        {
            StatusCode = StatusCodes.Status500InternalServerError;
        }

        public InternalServerErrorObjectResult(object error, int statusCode)
            : base(error)
        {
            StatusCode = statusCode;
        }
    }
}
