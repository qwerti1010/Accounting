using WebAccounting.Middlewares;

namespace WebAccounting.Extensions;

public static class TokenExtensions
{
    public static IApplicationBuilder UseToken(this IApplicationBuilder builder)
    {
        builder.UseMiddleware<TokenMiddleware>();
        return builder;
    }
}
