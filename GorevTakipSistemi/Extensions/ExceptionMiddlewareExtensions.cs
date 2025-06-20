using Entities.ErrorModel;
using Entities.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;
using System.Net;
using System.Runtime.CompilerServices;

namespace GorevTakipSistemi.Extensions
{
    public static class ExceptionMiddlewareExtensions
    {
        public static void ConfigureExceptionHandler(this WebApplication app, ILoggerService logger)
        {
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    context.Response.ContentType="application/json";
                    
                    var contextFeature=context.Features.Get<IExceptionHandlerFeature>();
                    var traceId=context.TraceIdentifier;

                    if (contextFeature is not null)
                    {
                        var exception = contextFeature.Error;

                        context.Response.StatusCode = contextFeature.Error switch
                        {
                            NotFoundException => StatusCodes.Status404NotFound,                           
                            UnauthorizedAccessException=>StatusCodes.Status401Unauthorized,
                            ArgumentNullException=>StatusCodes.Status400BadRequest,
                            _ => StatusCodes.Status500InternalServerError,
                        };

                                              
                        await logger.LogError($"TraceId:{traceId} || statusCode:{context.Response.StatusCode} || Exception: {exception}");

                        var errorDetails = new ErrorDetails
                        {
                            StatusCode = context.Response.StatusCode,
                            Message = contextFeature.Error.Message,
                            TraceId = traceId,
                            
                        };

                        await context.Response.WriteAsync(errorDetails.ToString());

                    }      
                });
            });
        }       
    }
}
