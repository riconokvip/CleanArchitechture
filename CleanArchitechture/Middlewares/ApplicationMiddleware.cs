using System.Net;

namespace CleanArchitechture.Middlewares
{
    public class ApplicationMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            BaseResponse response = null;

            if (exception is BaseException)
            {
                var ex = (BaseException)exception;
                response = new BaseResponse
                {
                    code = ex.httpCode,
                    error = ex.error,
                    message = ex.message
                };
            }
            else
            {
                response = new BaseResponse<Object>
                {
                    code = (int)HttpCodes.INTERNAL_SERVER_ERROR,
                    error = (int)ErrorCodes.ERROR,
                    message = EnumHelper<ErrorCodes>.GetDisplayValue(ErrorCodes.ERROR)
                };
            }

            var result = JsonConvert.SerializeObject(response);
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.OK;
            await context.Response.WriteAsync(result);
        }
    }
}
