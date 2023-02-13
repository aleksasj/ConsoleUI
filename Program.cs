using DataAccess.Data;
using DataAccess.DbAccess;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ConsoleUI;

class Program
{
    static void Main(string[] args)
    {
        var builder = new ConfigurationBuilder();
        BuildConfig(builder);

        var host = Host.CreateDefaultBuilder()
            .ConfigureServices((context, services) => {
                services.AddSingleton<ISqlDataAccess, SqlDataAccess>();
                services.AddSingleton<IUserRepository, UserRepository>();
                services.AddTransient<IConsoleAppService, ConsoleAppService>();
            })
            .Build();

        var svc = ActivatorUtilities.CreateInstance<ConsoleAppService>(host.Services);
        svc.Run();
        
    }

    static void BuildConfig(IConfigurationBuilder builder)
    {
        builder.SetBasePath(Directory.GetCurrentDirectory());
        builder.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
        builder.AddEnvironmentVariables();
    }

}