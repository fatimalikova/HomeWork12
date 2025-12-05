using ConsoleApp13.Helpers;
using ConsoleApp13.Models;
using ConsoleApp13.Services;
using PizzaApp.Services;

namespace PizzaApp.Menus
{
    public class AdminMenu
    {
        private readonly ProductService _productService;
        private readonly UserService _userService;

        public AdminMenu(ProductService productService, UserService userService)
        {
            _productService = productService;
            _userService = userService;
        }

        
        public void ShowAllProducts()
        {
            Console.WriteLine("\n--- PİZZALAR ---");

            foreach (var p in _productService.Products)
                Console.WriteLine($"{p.Id}. {p.Name} - {p.Price} AZN");

            Console.WriteLine("0 - Geri");
        }

    
        public void AddProduct()
        {
            Console.Write("Ad: ");
            string name = Console.ReadLine();

            Console.Write("Qiymət: ");
            decimal price = decimal.Parse(Console.ReadLine());

            Console.Write("İnqredientləri ',' ilə yaz: ");
            var ingredients = Console.ReadLine().Split(',').Select(i => i.Trim()).ToList();

            _productService.Add(new Product
            {
                Name = name,
                Price = price,
                Ingredients = ingredients
            });

            Console.WriteLine("Pizza əlavə edildi!");
        }

  
        public void UpdateProduct()
        {
            Console.Write("ID: ");
            int id = int.Parse(Console.ReadLine());

            var p = _productService.Products.FirstOrDefault(x => x.Id == id);
            if (p == null)
            {
                Console.WriteLine("Pizza tapılmadı!");
                return;
            }

            Console.Write("Yeni ad: ");
            p.Name = Console.ReadLine();

            Console.Write("Yeni qiymət: ");
            p.Price = decimal.Parse(Console.ReadLine());

            Console.Write("Yeni inqredientlər: ");
            p.Ingredients = Console.ReadLine().Split(',').Select(i => i.Trim()).ToList();

            _productService.Update(p);
            Console.WriteLine("Düzəliş edildi!");
        }

  
        public void DeleteProduct()
        {
            Console.Write("ID: ");
            int id = int.Parse(Console.ReadLine());

            _productService.Delete(id);
            Console.WriteLine("Silindi!");
        }


        // -----------------------------
        //    USER MENYUSU
        // -----------------------------

        public void ShowUsers()
        {
            Console.WriteLine("\n--- USERLƏR ---");

            foreach (var u in _userService.Users)
                Console.WriteLine($"{u.Id}. {u.Name} {u.Surname} - {u.Role}");

            Console.WriteLine("0 - Geri");
        }


        public void SetAdminRole()
        {
            Console.Write("ID: ");
            int id = int.Parse(Console.ReadLine());

            _userService.UpdateRole(id, UserRole.Admin);

            Console.WriteLine("Rol Admin edildi!");
        }

 
        public void RemoveAdminRole()
        {
            Console.Write("ID: ");
            int id = int.Parse(Console.ReadLine());

            _userService.UpdateRole(id, UserRole.User);

            Console.WriteLine("Rol User edildi!");
        }

        [Menu("User sil (ID)")]
        public void DeleteUser()
        {
            Console.Write("ID: ");
            int id = int.Parse(Console.ReadLine());

            _userService.Delete(id);

            Console.WriteLine("Silindi!");
        }
    }
}

