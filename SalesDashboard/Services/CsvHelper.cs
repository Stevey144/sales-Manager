using CsvHelper;
using CsvHelper.Configuration;
using SalesDashboard.Models;
using SalesDashboard.Services;
using System.Formats.Asn1;
using System.Globalization;
using System.Text;


namespace SalesDashboard.Services
{
    public static class CsvHelperService
    {
        private static string csvFilePath = "Data/Data.csv";

        public static List<Sale> ReadCsv()
        {
            if (!File.Exists(csvFilePath)) return new List<Sale>();

            using var reader = new StreamReader(csvFilePath);
            using var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                PrepareHeaderForMatch = args => args.Header.Trim(),
                HeaderValidated = null,
                MissingFieldFound = null,
                IgnoreBlankLines = true,
                BadDataFound = null
            });

            csv.Context.RegisterClassMap<SaleMap>();

            var records = csv.GetRecords<Sale>().ToList();

            bool hasIdColumn = File.ReadLines(csvFilePath).First().Split(',').Contains("Id");

            if (!hasIdColumn || records.Any(s => s.Id <= 0))
            {
                int idCounter = 1;
                foreach (var sale in records)
                {
                    sale.Id = idCounter++;  
                }
            }


            return records; 
        }

        public static void WriteCsv(List<Sale> sales)
        {
            var lines = new List<string> { "Id,Segment,Country,Product,Discount Band,Units Sold,Manufacturing Price,Sale Price,Date" };
            lines.AddRange(sales.Select(s => $"{s.Id},{s.Segment},{s.Country},{s.Product},{s.DiscountBand},{s.UnitsSold},{s.ManufacturingPrice},{s.SalePrice},{s.Date:yyyy-MM-dd}"));
            File.WriteAllLines(csvFilePath, lines);
        }

    }
}
