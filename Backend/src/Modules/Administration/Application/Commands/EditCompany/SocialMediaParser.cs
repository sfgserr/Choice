using System.Text.RegularExpressions;

namespace Administration.Application.Commands.EditCompany
{
    public static class SocialMediaParser
    {
        private static readonly Dictionary<string, Regex> PlatformPatterns = new()
        {
            { "Facebook", new Regex(@"^(https?:\/\/)?(www\.)?facebook\.com\/[A-Za-z0-9_\-.]+$", RegexOptions.IgnoreCase) },
            { "VK", new Regex(@"^(https?:\/\/)?(www\.)?vk\.com\/[A-Za-z0-9_\-.]+$", RegexOptions.IgnoreCase) },
            { "Telegram", new Regex(@"^(https?:\/\/)?(www\.)?t\.me\/[A-Za-z0-9_\-.]+$", RegexOptions.IgnoreCase) },
            { "Instagram", new Regex(@"^(https?:\/\/)?(www\.)?instagram\.com\/[A-Za-z0-9_\-.]+$", RegexOptions.IgnoreCase) }
        };
        
        public static string Parse(string url, Guid companyId)
        {
            if (string.IsNullOrWhiteSpace(url)) return string.Empty;

            foreach (var (platform, pattern) in PlatformPatterns)
            {
                if (pattern.IsMatch(url))
                {
                    return 
                        $"""
                        INSERT INTO users."SocialMedias" ("CompanyId", "Platform", "Url") VALUES ('{companyId}', '{platform}', '{url}');
                        """;
                }
            }

            throw new ArgumentException("Невалидная ссылка. Поддерживаемые платформы: Facebook, VK, Telegram и Instagram.");
        }
    }

}