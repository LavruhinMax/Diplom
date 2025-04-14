using System.Text.RegularExpressions;

namespace ISP_Desk.Service
{
    public static class FormatChecker
    {
        public static bool validateMail(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                return true;
            }
            var regex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");
            bool isValid = regex.IsMatch(email);
            return isValid;
        }

        public static bool validatePhone(string phone)
        {
            var phonePattern = @"^(?:\+7|8)\d{10}$";
            bool isValid = System.Text.RegularExpressions.Regex.IsMatch(phone, phonePattern);
            return isValid;
        }
    }
}
