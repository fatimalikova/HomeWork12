using ConsoleApp13.Data;
using ConsoleApp13.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp13.Services
{
    public class ProductService
    {
        private readonly string _path = "Data/products.json";
        public List<Product> Products { get; set; }

        public ProductService()
        {
            Products = JsonStorage.Load<Product>(_path);
        }

        public void Save() => JsonStorage.Save(_path, Products);

        public void Add(Product product)
        {
            product.Id = Products.Count == 0 ? 1 : Products.Max(x => x.Id) + 1;
            Products.Add(product);
            Save();
        }

        public void Update(Product updated)
        {
            var p = Products.FirstOrDefault(x => x.Id == updated.Id);
            if (p != null)
            {
                p.Name = updated.Name;
                p.Price = updated.Price;
                p.Ingredients = updated.Ingredients;
            }
            Save();
        }

        public void Delete(int id)
        {
            Products.RemoveAll(x => x.Id == id);
            Save();
        }
    }
}
