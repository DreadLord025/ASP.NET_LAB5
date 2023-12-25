using System.Text;
public class ErrorLoginMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger _logger;

    public ErrorLoginMiddleware(RequestDelegate next, ILogger<ErrorLoginMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            LogError(ex);
            throw;
        }
    }

    private void LogError(Exception ex)
    {
        _logger.LogError($"Ошибка: {ex}");

        try
        {
            string logMessage = $"{DateTime.Now} - Ошибка: {ex}\n";
            File.AppendAllText("errors.log", logMessage, Encoding.UTF8);
        }
        catch (Exception logEx)
        {
            _logger.LogError($"Ошибка при записи в журнал: {logEx}");
        }
    }
}

public static class ErrorLoginMiddlewareExtensions
{
    public static IApplicationBuilder UseErrorLoginMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<ErrorLoginMiddleware>();
    }
}
