using DataAccess.Data;
using DataAccess.Models;
using Microsoft.Extensions.Configuration;
using TaxiDispacher.Helpers;

namespace ConsoleUI;

public class ConsoleAppService : IConsoleAppService
{
    private readonly IUserRepository _userRepository;
    private string? _securitySalt = null;
    public ConsoleAppService(IUserRepository userRepository, IConfiguration config)
    {
        _userRepository = userRepository;
        _securitySalt = config["Salt"];
    }
    public void Run()
    {
        Console.Write("Let's create new ADMIN User:\n\n");

        Console.Write("Username: ");
        var username = Console.ReadLine();
        Console.Write("Password: ");
        var password = Console.ReadLine();
        try
        {
            _userRepository.Create(username, Crypt.Encrypt(password, _securitySalt), UsersModel.ROLE_ADMIN);

            Console.WriteLine(_userRepository.GetAll().Result.Count());
            Console.WriteLine("\n\nDone, try to login");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

        Console.WriteLine("\n\n");
    }

}
