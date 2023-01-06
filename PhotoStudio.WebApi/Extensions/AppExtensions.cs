using Microsoft.AspNetCore.Diagnostics;
using PhotoStudio.Infrastructure.Commons.CustomExceptions;
using System.Net;
using KeyNotFoundException = PhotoStudio.Infrastructure.Commons.CustomExceptions.KeyNotFoundException;

namespace PhotoStudio.WebApi.Extensions
{
    public static class AppExtensions
    {
        public static void UseCustomExceptionMiddleware(this IApplicationBuilder app)
        {

            app.UseExceptionHandler(appRun =>
            {
                appRun.Run(async (context) =>
                {
                    context.Response.ContentType = "application/json";
                    context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                    await context.Response.WriteAsync("Something went wrong");

                    ErrorResponseDTO responseDTO = new ErrorResponseDTO();
                    responseDTO.Success = false;

                    var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();

                    switch (exceptionHandlerPathFeature?.Error)
                    {
                        case AppException error:

                            responseDTO.StatusCode = (int)HttpStatusCode.BadRequest;
                            responseDTO.Message = error.Message;
                            break;
                        case KeyNotFoundException error:
                            responseDTO.StatusCode = (int)HttpStatusCode.NotFound;
                            responseDTO.Message = error.Message;
                            break;
                        default:
                            responseDTO.StatusCode = (int)HttpStatusCode.InternalServerError;
                            responseDTO.Message = exceptionHandlerPathFeature?.Error?.Message ?? "Internal Server Error";
                            break;
                    }

                    await context.Response.WriteAsync(responseDTO.ToString());

                });
            });

        }
    }
}
