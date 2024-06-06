using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hangfire;

namespace FieldRent.Middlewares
{
    public class CustomMiddleware
    {

    private readonly RequestDelegate _next;

    public CustomMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {


        RecurringJob.AddOrUpdate<Jobs.FinishRent>("print-message-job", x => x.PrintMessage(), cronExpression: "* * * * *");



        // Sonraki middleware veya endpoint'e yönlendirme
        await _next.Invoke(context);
    }
}

// Middleware'yi uygulamak için bir extension method
public static class CustomMiddlewareExtensions
{
    public static IApplicationBuilder UseCustomMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<CustomMiddleware>();
    }
}
    }
