using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

public class ClientIpMiddleware
{
    private readonly RequestDelegate _next;

    public ClientIpMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var ipAddress = context.Connection.RemoteIpAddress?.ToString();
        var logger = context.RequestServices.GetRequiredService<ILogger<ClientIpMiddleware>>();
        logger.LogInformation($"Client IP: {ipAddress}");
        await _next(context);
    }
}
