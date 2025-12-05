using System.Linq;

namespace ConsoleApp13.Helpers
{
    public static class Validator
    {
       
        public static bool ValidateLogin(string login)
        {
            if (string.IsNullOrWhiteSpace(login)) return false;

            login = login.Trim();

            const int minLength = 3;
            const int maxLength = 50;
            if (login.Length < minLength || login.Length > maxLength) return false;

            foreach (char c in login)
            {
                if (char.IsLetterOrDigit(c)) continue;
                if (c == '.' || c == '_' || c == '-') continue;
                return false;
            }

            return true;
        }

        public static bool ValidatePassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password)) return false;

            password = password.Trim();

            const int minLength = 6;
            if (password.Length < minLength) return false;

            bool hasLetter = false;
            bool hasDigit = false;

            foreach (char c in password)
            {
                if (char.IsLetter(c)) hasLetter = true;
                else if (char.IsDigit(c)) hasDigit = true;

                if (hasLetter && hasDigit) break;
            }

            return hasLetter && hasDigit;
        }
    }
}
