using System.Linq;

namespace PizzaApp.Helpers
{
    public static  class Validator
    {
        public static bool ValidateLogin(string login)
        {
            return !string.IsNullOrWhiteSpace(login)
                && login.Length >= 3
                && login.Length <= 16;
        }

        public static bool ValidatePassword(string pass)
        {
            if (string.IsNullOrWhiteSpace(pass))
                return false;

            if (pass.Length < 6 || pass.Length > 16)
                return false;

            return pass.Any(char.IsUpper)
                && pass.Any(char.IsLower)
                && pass.Any(char.IsDigit);
        }
    }
}
