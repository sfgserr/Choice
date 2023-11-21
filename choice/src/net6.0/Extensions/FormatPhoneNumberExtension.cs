using System.Text.RegularExpressions;

namespace Choice.Extensions
{
    public static class FormatPhoneNumberExtension
    {
        public static string FormatPhoneNumber(this string phoneNumber)
        {
            phoneNumber = new Regex(@"\D").Replace(phoneNumber, string.Empty);

            if (phoneNumber.Length == 0)
                phoneNumber = string.Empty;
            else if (phoneNumber.Length < 3)
                phoneNumber = string.Format("({0})", phoneNumber.Substring(0, phoneNumber.Length));
            else if (phoneNumber.Length < 7)
                phoneNumber = string.Format("({0}) {1}", phoneNumber.Substring(0, 3), phoneNumber.Substring(3, phoneNumber.Length - 3));
            else if (phoneNumber.Length < 9)
                phoneNumber = string.Format("({0}) {1}-{2}", phoneNumber.Substring(0, 3), phoneNumber.Substring(3, 3), phoneNumber.Substring(6));
            else if (phoneNumber.Length < 11)
                phoneNumber = string.Format("({0}) {1}-{2}-{3}", phoneNumber.Substring(0, 3), phoneNumber.Substring(3, 3), phoneNumber.Substring(6, 2), phoneNumber.Substring(8));
            return phoneNumber;
        }
    }
}
