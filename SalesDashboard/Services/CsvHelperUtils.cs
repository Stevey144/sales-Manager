using System.Globalization;
using System.Text.RegularExpressions;

namespace SalesDashboard.Services
{
    public static class CsvHelperUtils
    {


        public static decimal ConvertDecimal(string input)
        {
            if (string.IsNullOrWhiteSpace(input)) return 0.0m;

            string cleanText = Regex.Replace(input, @"[^\d.-]", "");
            if (decimal.TryParse(cleanText, NumberStyles.Any, CultureInfo.InvariantCulture, out var value))
            {
                return value;
            }

            Console.WriteLine($"Invalid decimal value: {input}"); // Log issues
            return 0.0m;
        }

        public static DateTime ConvertDate(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                return DateTime.MinValue; 
            }

            var formats = new[] { "d/MM/yyyy", "MM/dd/yyyy", "yyyy-MM-dd" }; 
            if (DateTime.TryParseExact(input, formats, CultureInfo.InvariantCulture, DateTimeStyles.None, out var parsedDate))
            {
                return parsedDate;
            }

            Console.WriteLine($"Invalid date format: {input}");
            return DateTime.MinValue;
        }
    }
}
