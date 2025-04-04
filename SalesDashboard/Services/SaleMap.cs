using CsvHelper.Configuration;
using CsvHelper.TypeConversion;
using SalesDashboard.Models;
using SalesDashboard.Services;
using System.Globalization;

namespace SalesDashboard.Services
{
    public class SaleMap : ClassMap<Sale>
    {
        public SaleMap()
        {
            Map(m => m.Id).Name("Id");
            Map(m => m.Segment).Name("Segment");
            Map(m => m.Country).Name("Country");
            Map(m => m.Product).Name("Product");
            Map(m => m.DiscountBand).Name("Discount Band");
            Map(m => m.UnitsSold).Name("Units Sold").Convert(args => CsvHelperUtils.ConvertDecimal(args.Row.GetField("Units Sold")));
            Map(m => m.ManufacturingPrice).Name("Manufacturing Price").Convert(args => CsvHelperUtils.ConvertDecimal(args.Row.GetField("Manufacturing Price")));
            Map(m => m.SalePrice).Name("Sale Price").Convert(args => CsvHelperUtils.ConvertDecimal(args.Row.GetField("Sale Price")));
            Map(m => m.Date).Name("Date").Convert(args => CsvHelperUtils.ConvertDate(args.Row.GetField("Date"))); // Custom date parsing

        }
    }
}
