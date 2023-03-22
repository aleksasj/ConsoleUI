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
        BuildConfig(new ConfigurationBuilder());

        var host = Host.CreateDefaultBuilder()
            .ConfigureServices((context, services) =>
            {
                services.AddSingleton<ISqlDataAccess, SqlDataAccess>();
                services.AddSingleton<IUserRepository, UserRepository>();
                services.AddTransient<IConsoleAppService, ConsoleAppService>();
            })
            .Build();

        ActivatorUtilities.CreateInstance<ConsoleAppService>(host.Services).Run();
    }

    static void BuildConfig(IConfigurationBuilder builder)
    {
        builder.SetBasePath(Directory.GetCurrentDirectory());
        builder.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
        builder.AddEnvironmentVariables();
    }

}