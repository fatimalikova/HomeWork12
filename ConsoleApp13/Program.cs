using ConsoleApp13.Helpers;
using ConsoleApp13.Menu;
using ConsoleApp13.Models;
using ConsoleApp13.Services;
using PizzaApp.Menus;
using PizzaApp.Services;
using MyValidator = PizzaApp.Helpers.Validator;   // <-- Konflikti aradan qaldıran alias

internal class Program
{
    static UserService userService = new UserService();
    static ProductService productService = new ProductService();

    private static void Main(string[] args)
    {
        while (true)
        {
            Console.WriteLine("\n1. Login\n2. Qeydiyyat");
            Console.Write("Seç: ");
            string choice = Console.ReadLine() ?? string.Empty;

            if (choice == "1") Login();
            else if (choice == "2") Register();
            else Console.WriteLine("Yanlış seçim!");
        }
    }

    static void Register()
    {
        Console.Write("Ad: ");
        string name = Console.ReadLine() ?? string.Empty;

        Console.Write("Soyad: ");
        string surname = Console.ReadLine() ?? string.Empty;

        Console.Write("Login: ");
        string login = Console.ReadLine() ?? string.Empty;

        if (!MyValidator.ValidateLogin(login))
        {
            Console.WriteLine("Login yanlışdır!");
            return;
        }

        Console.Write("Parol: ");
        string pass = Console.ReadLine() ?? string.Empty;

        if (!MyValidator.ValidatePassword(pass))
        {
            Console.WriteLine("Parol yanlışdır!");
            return;
        }

        userService.Add(new User
        {
            Name = name,
            Surname = surname,
            Login = login,
            Password = pass,
            Role = UserRole.User
        });

        Console.WriteLine("Qeydiyyat tamamlandı!");
    }

    static void Login()
    {
        Console.Write("Login: ");
        string login = Console.ReadLine() ?? string.Empty;

        Console.Write("Parol: ");
        string pass = Console.ReadLine() ?? string.Empty;

        var user = userService.GetByLogin(login);

        if (user == null || user.Password != pass)
        {
            Console.WriteLine("Yanlış məlumat.");
            return;
        }

        Console.WriteLine($"Xoş gəldiniz, {user.Name} {user.Surname}");

        if (user.Role == UserRole.Admin)
        {
            AdminMenu adminMenu = new(productService, userService);
            MenuEngine.ShowMenu(adminMenu);
        }
        else
        {
            UserMenu userMenu = new(productService, user);
            MenuEngine.ShowMenu(userMenu);
        }
    }
}
