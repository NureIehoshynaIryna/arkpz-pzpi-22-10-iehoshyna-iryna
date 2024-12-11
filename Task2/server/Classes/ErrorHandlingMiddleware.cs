﻿using System.Net;

namespace server.Classes;

public class ErrorHandlingMiddleware
{
    public class ErrorResult
    {
        public int ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
    }

    private readonly RequestDelegate next;

    public ErrorHandlingMiddleware(RequestDelegate next)
    {

        this.next = next;
    }

    public async Task Invoke(HttpContext context)
    {

        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            // Logger.Error(ex);
            ErrorResult errorResult;
            if (ex is DomainException domainException)
            {
                errorResult = new ErrorResult { ErrorCode = (int)domainException.ErrorCode, ErrorMessage = domainException.Message };
            }
            else
            {
                errorResult = new ErrorResult { ErrorCode = (int)HttpStatusCode.InternalServerError, ErrorMessage = ex.Message };
            }
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = errorResult.ErrorCode;
            var result = System.Text.Json.JsonSerializer.Serialize(errorResult);
            await context.Response.WriteAsync(result);
        }
    }
}