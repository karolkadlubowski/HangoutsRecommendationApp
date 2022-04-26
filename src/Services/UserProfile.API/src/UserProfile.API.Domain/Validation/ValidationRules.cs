using System.Text.RegularExpressions;

namespace UserProfile.API.Domain.Validation
{
    public static class ValidationRules
    {
        public static string emailAdressRegex = "(^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+.[a-zA-Z0-9-.]+$)";
    }
}