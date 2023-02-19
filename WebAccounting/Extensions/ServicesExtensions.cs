using DBLibrary;
using Services.Services;

namespace WebAccounting.Extensions;

public static class ServicesExtensions
{
    public static IServiceCollection AddMyServices(this IServiceCollection services)
    {
        services.AddScoped<DbConnect>();
        services.AddScoped<EmployeeService>();
        services.AddScoped<ComputerService>();
        services.AddScoped<DBService>();
        return services;
    }
}
