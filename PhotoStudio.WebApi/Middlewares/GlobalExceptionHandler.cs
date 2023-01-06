using Microsoft.Extensions.Options;
using PhotoStudio.Infrastructure.Commons.Configurations;
using PhotoStudio.Infrastructure.Commons.CustomExceptions;
using System.Net;
using System.Text.Json;
using KeyNotFoundException = PhotoStudio.Infrastructure.Commons.CustomExceptions.KeyNotFoundException;

namespace PhotoStudio.WebApi.Middlewares
{
    public class GlobalExceptionHandler
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionHandler> _logger;
        private readonly IOptions<CustomExceptionHandlerOptions> _execeptionOptions;

        public GlobalExceptionHandler(RequestDelegate next, ILogger<GlobalExceptionHandler> logger, IOptions<CustomExceptionHandlerOptions> execeptionOptions)
        {
            _next = next;
            _logger = logger;
            _execeptionOptions = execeptionOptions;
        }

        public async Task InvokeAsync(HttpContext context)
        {

            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }

        }
        public async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            var response = context.Response;

            var errorResponse = new ErrorResponseDTO
            {
                Success = false
            };

            switch (exception)
            {

                case ApplicationException ex:
                    errorResponse.StatusCode = (int)HttpStatusCode.BadRequest;
                    errorResponse.Message = ex.Message;
                    break;
                case AppException ex:
                    errorResponse.StatusCode = (int)HttpStatusCode.BadRequest;
                    errorResponse.Message = ex.Message;
                    break;
                case KeyNotFoundException ex:
                    errorResponse.StatusCode = (int)HttpStatusCode.NotFound;
                    errorResponse.Message = ex.Message;
                    break;
                default:
                    errorResponse.StatusCode = (int)HttpStatusCode.InternalServerError;
                    errorResponse.Message = exception.Message;
                    break;

            }

            if(_execeptionOptions.Value.AllwaysReturnOK)
                errorResponse.StatusCode = (int)HttpStatusCode.OK;
            if (_execeptionOptions.Value.IncludeDetails)
                errorResponse.ErrorDetails = exception.StackTrace ?? string.Empty;

            string resp = JsonSerializer.Serialize(errorResponse);
            _logger.LogError(errorResponse.Message);
            await response.WriteAsync(resp);
        }
    }
}
