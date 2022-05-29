using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Threading.Tasks;

namespace ProductsInventory.Middlewares
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class GroupTwoExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public GroupTwoExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {

            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext httpContext, Exception ex)
        {
            ExceptionResponseWrapper exceptionWrapper = new ExceptionResponseWrapper();
            int statusCode = (int)HttpStatusCode.OK;

            if (ex is UnauthorizedAccessException)
            {
                exceptionWrapper.Code = (int)HttpStatusCode.Unauthorized;
                exceptionWrapper.Message = "NO TIENES ACCESO";
            }

            httpContext.Response.ContentType = "application/json";
            httpContext.Response.Headers["Accept"] = "application/json";
            httpContext.Response.StatusCode = statusCode;

            //Log.Error("Ha sucedido un error en la aplicacion" + ex.Message);

            return httpContext.Response.WriteAsync(JsonConvert.SerializeObject(exceptionWrapper));
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class GroupTwoExceptionMiddlewareExtensions
    {
        public static IApplicationBuilder UseGroupTwoExceptionMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<GroupTwoExceptionMiddleware>();
        }
    }
}
