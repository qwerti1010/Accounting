namespace WebAccounting.Middlewares;

public class TokenMiddleware
{
    public readonly RequestDelegate _next;

    public TokenMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var token = context.Request.Query["token"];
       
        if (token.ToString() != "1234")
        {
            context.Response.StatusCode = 403;
            await context.Response.WriteAsync($"Token is invalid: {token}");
        }
        else
        {
            Console.WriteLine($"Token is valid: {token}");
            await _next.Invoke(context);
        }
    }
}
