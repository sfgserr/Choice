using System.Text;

namespace Authentication.Api.Services
{
    public class CodeManager
    {
        private static readonly Dictionary<string, string> _codes = [];

        public static string GenerateCode(string key)
        {
            var random = new Random();

            var stringBuilder = new StringBuilder();

            do
            {
                stringBuilder.Clear();

                for (int i = 0; i < 6; i++)
                    stringBuilder.Append(random.Next(1, 10));
            }
            while (_codes.ContainsValue(stringBuilder.ToString()));

            string code = stringBuilder.ToString();

            if (!_codes.TryAdd(key, code))
                _codes[key] = code;

            return code;
        }

        public static bool Check(string key, string code)
        {
            bool isMatched = _codes.TryGetValue(key, out var value) && value == code;

            if (isMatched)
                _codes.Remove(key);

            return isMatched;
        }
    }
}
