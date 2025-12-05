using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp13.Data
{
    public static  class JsonStorage
    {
        public static List<T> Load<T>(string filePath)
        {
            if (!File.Exists(filePath))
            {
                return new List<T>();
            }
            var json = File.ReadAllText(filePath);
            return System.Text.Json.JsonSerializer.Deserialize<List<T>>(json) ?? new List<T>();
        }


        public static void Save<T>(string filePath, List<T> data)
        {
            var json = System.Text.Json.JsonSerializer.Serialize(data, new System.Text.Json.JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(filePath, json);
        }
    }
}
