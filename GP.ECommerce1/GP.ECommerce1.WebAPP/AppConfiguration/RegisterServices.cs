using Blazored.LocalStorage;
using Blazored.Toast;
using DevExpress.Blazor.Reporting;
namespace GP.ECommerce1.BlazorWebApp.AppConfiguration;

public static class RegisterServices
{
    public static void AddAppServices(this IServiceCollection services, AppSettings appSettings)
    {
        services.AddBlazoredToast();
        AddHttpClients(services, appSettings);
        services.AddBlazoredLocalStorage();
        services.AddDevExpress();
    }

    private static void AddHttpClients(IServiceCollection services, AppSettings appSettings)
    {
    }

    private static void AddDevExpress(this IServiceCollection services)
    {
        services.AddDevExpressBlazor(configure => configure.BootstrapVersion = DevExpress.Blazor.BootstrapVersion.v5);
    }
}