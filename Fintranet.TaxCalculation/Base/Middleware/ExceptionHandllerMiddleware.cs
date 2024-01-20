using Fintranet.TaxCalculation.Api.Base.Helpers;
using Fintranet.TaxCalculation.Repository.LogInterface;
using Newtonsoft.Json;
using System.Net;

namespace Fintranet.TaxCalculation.Api.Base.Middleware
{
    public class ExceptionHandllerMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandllerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, IExceptionLogger exceptionLogger)
        {
            var requestUrl = context.Request.GetFullUrl();

            try
            {
                await _next.Invoke(context);
            }
            catch (Exception exception)
            {
                await ManageException(context, exceptionLogger, exception);
                throw;
            }
        }

        private async Task ManageException(HttpContext context, IExceptionLogger exceptionLogger, Exception ex)
        {
            switch (ex)
            {
                case Exception exception:
                    {
                        await ConfigureResponse(context, HttpStatusCode.InternalServerError, "متاسفانه خطای سیستمی رخ داده است، در صورت لزوم با پشتیبانی تماس حاصل نمایید");
                        await exceptionLogger.SetLog(exception.Message, exception.StackTrace);
                        break;
                    }
                default:
                    break;
            }
        }

        private static async Task ConfigureResponse(HttpContext context, HttpStatusCode statusCode, string message)
        {
            context.Response.StatusCode = (int)statusCode;
            context.Response.ContentType = "application/json";

            await context.Response.WriteAsync(
                new FailedResponseMessage(message).ToString());
        }

        public class FailedResponseMessage
        {
            public FailedResponseMessage(string message)
            {
                this.message = message;
            }
            public string message { get; set; }
            public override string ToString()
            {
                return JsonConvert.SerializeObject(this);
            }
        }
    }
}
