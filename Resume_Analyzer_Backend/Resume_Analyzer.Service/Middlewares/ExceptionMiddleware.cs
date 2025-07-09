using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using ServiceLayer.Exceptions;

namespace Resume_Analyzer.Service.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
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
        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            int statusCode;
            string errorCode;

            switch (exception)
            {
                case BadRequestException:
                    statusCode = StatusCodes.Status400BadRequest;
                    errorCode = "BAD_REQUEST";
                    break;

                case NotFoundException:
                    statusCode = StatusCodes.Status404NotFound;
                    errorCode = "NOT_FOUND";
                    break;

                case UnauthorizedAccessException:
                    statusCode = StatusCodes.Status401Unauthorized;
                    errorCode = "UNAUTHORIZED";
                    break;

                default:
                    statusCode = StatusCodes.Status500InternalServerError;
                    errorCode = "INTERNAL_SERVER_ERROR";

                    while (exception.InnerException != null)
                    {
                        exception = exception.InnerException;
                    }
                    break;
            }

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = statusCode;

            return context.Response.WriteAsync(JsonSerializer.Serialize(new
            {
                statusCode,
                errorCode,
                message = exception.Message
            }));
        }
    }
}
