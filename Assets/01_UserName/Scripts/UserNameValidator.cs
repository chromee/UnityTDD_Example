using System.Text.RegularExpressions;

namespace Project.Scripts
{
    public class UserNameValidator
    {
        public static bool Validate(string name)
        {
            Regex regex = new Regex(@"^[a-zA-Z]{1,8}$");
            return regex.IsMatch(name);
        }
    }
    
    
}