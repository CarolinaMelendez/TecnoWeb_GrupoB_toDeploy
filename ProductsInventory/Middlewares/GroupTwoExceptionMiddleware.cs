using DB.Exceptions;
using Logic.Exceptions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Services.Exceptions;
using System;
using System.Net;
using System.Threading.Tasks;

namespace ProductsInventory.Middlewares
{
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
                exceptionWrapper.Message = "No tienes acceso";
            }
            else if (ex.InnerException is PriceServiceException)
            {
                exceptionWrapper.Code = (int)HttpStatusCode.OK;
                exceptionWrapper.Message = $"Hay errores de conexion con servicios externos. MORE DETAIL: {ex.Message} ";
            }else if (ex.InnerException is DatabaseException || ex is DatabaseException)
            {
                exceptionWrapper.Code = (int)HttpStatusCode.OK;
                exceptionWrapper.Message = $"Hay errores de conexion con Base de Datos. MORE DETAIL: {ex.Message} ";
            }else if (ex is InvalidTypeException)
            {
                exceptionWrapper.Code = (int)HttpStatusCode.OK;
                exceptionWrapper.Message = $"Hay errores de Tipo de Producto. MORE DETAIL: {ex.Message} ";
            }else if(ex is InvalidStockException)
            {
                exceptionWrapper.Code = (int)HttpStatusCode.OK;
                exceptionWrapper.Message = $"Hay errores de Stock. MORE DETAIL: {ex.Message} ";
            }
            else
            {
                exceptionWrapper.Code = (int)HttpStatusCode.InternalServerError;
                exceptionWrapper.Message = "Ha sucedido un error inesperado : " + ex.Message;
            }
            
            httpContext.Response.ContentType = "application/json";
            httpContext.Response.Headers["Accept"] = "application/json";
            httpContext.Response.StatusCode = statusCode;

            //Log.Error("Ha sucedido un error en la aplicacion" + ex.Message);

            return httpContext.Response.WriteAsync(JsonConvert.SerializeObject(exceptionWrapper));
        }
    }

    public static class GroupTwoExceptionMiddlewareExtensions
    {
        public static IApplicationBuilder UseGroupTwoExceptionMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<GroupTwoExceptionMiddleware>();
        }
    }
}
