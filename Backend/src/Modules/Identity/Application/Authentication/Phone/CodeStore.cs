using BuildingBlocks.Application.Exceptions;

namespace Identity.Application.Authentication.Phone
{
    internal static class CodeStore
    {
        private static Dictionary<Guid, Code> _codes = new();

        public static string Add(Guid userId)
        {
            var code = Code.Generate(userId);
            
            var isAdded = _codes.TryAdd(userId, code);

            if (!isAdded && _codes[code.UserId].ExpirationDate < DateTime.UtcNow)
            {
                _codes[code.UserId] = code;
                return code.Value;
            }

            return isAdded ? code.Value : throw new InvalidCommandException(["Код уже выслан"]);
        }

        public static bool Verify(Guid userId, string verifyingCode)
        {
            _codes.TryGetValue(userId, out var code);

            return code != null && verifyingCode == code.Value;
        }
    }
}