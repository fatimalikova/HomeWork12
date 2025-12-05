using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp13.Helpers
{
    public class MenuEngine
    {
        public static void ShowMenu(object menuObject)
        {
            var methods = menuObject.GetType()
                .GetMethods()
                .Where(m => m.GetCustomAttributes(typeof(MenuAttribute), false).Any())
                .ToList();

            while (true)
            {
                Console.WriteLine("\n--- MENYU ---");
                int i = 1;

                foreach (var method in methods)
                {
                    var attr = (MenuAttribute)method.GetCustomAttributes(typeof(MenuAttribute), false).First();
                    Console.WriteLine($"{i}. {attr.Title}");
                    i++;
                }

                Console.Write("Seçim: ");
                int choice = int.Parse(Console.ReadLine());

                if (choice < 1 || choice > methods.Count)
                {
                    Console.WriteLine("Yanlış seçim!");
                    continue;
                }

                methods[choice - 1].Invoke(menuObject, null);
            }
        }
    }

    [AttributeUsage(AttributeTargets.Method)]
    public class MenuAttribute : Attribute
    {
        public string Title { get; }
        public MenuAttribute(string title) => Title = title;
    }

}
