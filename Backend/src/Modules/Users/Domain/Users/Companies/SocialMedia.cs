using System.Text.RegularExpressions;
using BuildingBlocks.Domain;

namespace Users.Domain.Users.Companies
{
    public class SocialMedia : ValueObject
    {
        private static readonly Dictionary<string, Regex> PlatformPatterns = new()
        {
            { "Facebook", new Regex(@"^(https?:\/\/)?(www\.)?facebook\.com\/[A-Za-z0-9_\-.]+$", RegexOptions.IgnoreCase) },
            { "VK", new Regex(@"^(https?:\/\/)?(www\.)?vk\.com\/[A-Za-z0-9_\-.]+$", RegexOptions.IgnoreCase) },
            { "Telegram", new Regex(@"^(https?:\/\/)?(www\.)?t\.me\/[A-Za-z0-9_\-.]+$", RegexOptions.IgnoreCase) },
            { "Instagram", new Regex(@"^(https?:\/\/)?(www\.)?instagram\.com\/[A-Za-z0-9_\-.]+$", RegexOptions.IgnoreCase) }
        };

        public string Url { get; }
        
        public string Platform { get; }

        private SocialMedia(string url, string platform)
        {
            Url = url;
            Platform = platform;
        }
        
        private SocialMedia() {}
        
        public static SocialMedia Create(string url)
        {
            if (string.IsNullOrWhiteSpace(url))
                throw new ArgumentException("Ссылка не может быть пустой");

            foreach (var (platform, pattern) in PlatformPatterns)
            {
                if (pattern.IsMatch(url))
                {
                    return new SocialMedia(url, platform);
                }
            }

            throw new ArgumentException("Невалидная ссылка. Поддерживаемые платформы: Facebook, VK, Telegram и Instagram.");
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Platform;
            yield return Url;
        }

        public override string ToString() => $"{Platform}: {Url}";
    }

}