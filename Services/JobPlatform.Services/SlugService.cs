namespace JobPlatform.Services
{
    using System;
    using System.Globalization;
    using System.Text.RegularExpressions;

    public class SlugService : ISlugService
    {
        public string ConvertSlug(string slug)
        {
            slug = ConvertCyrillicToLatinLetters(slug).Trim().ToLower();

            // Replace spaces with dashes
            slug = slug.Replace(" ", "-").Replace("--", "-").Replace("--", "-");

            // Remove non-letter characters
            slug = Regex.Replace(slug, "[^a-zA-Z0-9_-]+", string.Empty, RegexOptions.Compiled);

            // Trim length to 100 chars
            return slug.Substring(0, Math.Min(100, slug.Length)).Trim('-');
        }

        private static string ConvertCyrillicToLatinLetters(string input)
        {
            var bulgarianLetters = new[]
                                   {
                                       "а", "б", "в", "г", "д", "е", "ж", "з", "и", "й", "к", "л", "м", "н", "о", "п",
                                       "р", "с", "т", "у", "ф", "х", "ц", "ч", "ш", "щ", "ъ", "ь", "ю", "я",
                                   };
            var latinRepresentationsOfBulgarianLetters = new[]
                                                         {
                                                             "a", "b", "v", "g", "d", "e", "j", "z", "i", "y", "k",
                                                             "l", "m", "n", "o", "p", "r", "s", "t", "u", "f", "h",
                                                             "c", "ch", "sh", "sht", "u", "i", "yu", "ya",
                                                         };
            for (var i = 0; i < bulgarianLetters.Length; i++)
            {
                input = input.Replace(bulgarianLetters[i], latinRepresentationsOfBulgarianLetters[i]);
                input = input.Replace(bulgarianLetters[i].ToUpper(), CapitalizeFirstLetter(latinRepresentationsOfBulgarianLetters[i]));
            }

            return input;
        }

        private static string CapitalizeFirstLetter(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return input;
            }

            return input.Substring(0, 1).ToUpper(CultureInfo.CurrentCulture) + input.Substring(1);
        }
    }
}
