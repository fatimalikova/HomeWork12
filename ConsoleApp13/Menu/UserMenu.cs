using ConsoleApp13.Helpers;
using ConsoleApp13.Models;
using ConsoleApp13.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp13.Menu
{
    public class UserMenu
    {
        private readonly ProductService _productService;
        private readonly User currentUser;
        private List<Product> basket = new();

        public UserMenu(ProductService productService, User user)
        {
            _productService = productService;
            currentUser = user;
        }

       
        public void ShowProducts()
        {
            foreach (var p in _productService.Products)
                Console.WriteLine($"{p.Id}. {p.Name}");

            Console.Write("Seç (0 geri): ");
            int id = int.Parse(Console.ReadLine());
            if (id == 0) return;

            var product = _productService.Products.FirstOrDefault(x => x.Id == id);
            if (product == null) return;

            Console.WriteLine("Tərkibi:");
            product.Ingredients.ForEach(i => Console.WriteLine("- " + i));

            Console.Write("S - əlavə et, G - geri: ");
            var key = Console.ReadLine().ToUpper();

            if (key == "S")
            {
                Console.Write("Say: ");
                int count = int.Parse(Console.ReadLine());
                for (int i = 0; i < count; i++) basket.Add(product);
                Console.WriteLine("Səbətə əlavə olundu!");
            }
        }

     
        public void Order()
        {
            decimal total = basket.Sum(x => x.Price);
            Console.WriteLine($"Məbləğ: {total} AZN");

            Console.Write("Ünvan: ");
            string address = Console.ReadLine();

            Console.Write("Telefon: ");
            string phone = Console.ReadLine();

            Console.WriteLine("Sifariş qəbul edildi!");
            basket.Clear();
        }
    }

}
