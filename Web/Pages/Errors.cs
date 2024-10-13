using Microsoft.AspNetCore.Http.Extensions;

namespace Web.Pages;

public static class Errors
{
    public static async Task Return500(HttpContext context)
    {
        context.Response.StatusCode = 500;
        context.Response.ContentType = "text/html; charset=utf-8";
        await context.Response.WriteAsync(
            $"<h1>Processing of the request failed. Please provide technical support with the URL, time and request ID</h1><h2>URL: {context.Request.GetEncodedUrl()}<br/>Time: {DateTime.Now}<br/>Request ID: {context.TraceIdentifier}<br/>Technical support contact: </h2>");
    }
}