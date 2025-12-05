using ConsoleApp13.Helpers;
using ConsoleApp13.Menu;
using ConsoleApp13.Models;
using ConsoleApp13.Services;
using PizzaApp.Menus;
using PizzaApp.Services;
using System.ComponentModel.DataAnnotations;
using Validator = ConsoleApp13.Helpers.Validator;

namespace ConsoleApp13
{
    class Program
    {
        static UserService userService = new();
        static ProductService productService = new();

        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("\n1. Login\n2. Qeydiyyat");
                Console.Write("Seç: ");
                string choice = Console.ReadLine() ?? string.Empty;

                if (choice == "1") Login();
                else if (choice == "2") Register();
            }
        }

        static void Register()
        {
            Console.Write("Ad: ");
            string name = (Console.ReadLine() ?? string.Empty).Trim();

            Console.Write("Soyad: ");
            string surname = (Console.ReadLine() ?? string.Empty).Trim();

            Console.Write("Login: ");
            string login = (Console.ReadLine() ?? string.Empty).Trim();
            if (Validator.ValidateLogin(login))
            {
                Console.Write("Parol: ");
                string pass = (Console.ReadLine() ?? string.Empty).Trim();
                if (Validator.ValidatePassword(pass))
                {
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
                else
                { Console.WriteLine("Parol yanlışdır!"); return; }
            }
            else
            { Console.WriteLine("Login yanlışdır!"); return; }
        }

        static void Login()
        {
            Console.Write("Login: ");
            string login = (Console.ReadLine() ?? string.Empty).Trim();

            Console.Write("Parol: ");
            string pass = (Console.ReadLine() ?? string.Empty).Trim();

           

            var user = userService.GetByLogin(login);

            if (user == null || (user.Password?.Trim() ?? string.Empty) != pass)
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
}
