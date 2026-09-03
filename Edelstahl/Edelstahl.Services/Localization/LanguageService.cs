using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


namespace Edelstahl.Services.Localization
{
    public static class LanguageService
    {
        public static string Translate(
            string key,
            string languageCode)
        {
            string filePath =
                Path.Combine(
                    AppDomain.CurrentDomain.BaseDirectory,
                    "Languages",
                    $"idioma.{languageCode}");

            if (!File.Exists(filePath))
            {
                return filePath;
            }

            string[] lines =
                File.ReadAllLines(filePath);

            foreach (string line in lines)
            {
                string[] parts =
                    line.Split(':');

                if (parts.Length != 2)
                {
                    continue;
                }

                if (string.Equals(
                    parts[0].Trim(),
                    key,
                    StringComparison.OrdinalIgnoreCase))
                {
                    return parts[1].Trim();
                }
            }

            return key;
        }
    }
}

