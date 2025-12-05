using System;
using System.Linq;
using ConsoleApp13.Data;
using ConsoleApp13.Models;

namespace PizzaApp.Services
{
    public class UserService
    {
        private readonly string _path = "Data/users.json";
        public List<User> Users { get; set; }

        public UserService()
        {
            Users = JsonStorage.Load<User>(_path);

            if (Users == null)
                Users = new List<User>();
        }

        public void Save() => JsonStorage.Save(_path, Users);

      
        public User? GetByLogin(string login) =>
            Users.FirstOrDefault(x =>
                string.Equals(x.Login?.Trim(), login?.Trim(), StringComparison.OrdinalIgnoreCase));

        public void Add(User user)
        {
            user.Id = Users.Count == 0 ? 1 : Users.Max(x => x.Id) + 1;
            Users.Add(user);
            Save();
        }

        public void UpdateRole(int id, UserRole role)
        {
            var user = Users.FirstOrDefault(x => x.Id == id);
            if (user != null)
                user.Role = role;

            Save();
        }

        public void Delete(int id)
        {
            Users.RemoveAll(x => x.Id == id);
            Save();
        }
    }
}
