using CsvHelper.Configuration.Attributes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SalesDashboard.Models
{
    public class Sale
    {
        public int Id { get; set; }
        public string Segment { get; set; }
        public string Country { get; set; }
        public string Product { get; set; }
        public string DiscountBand { get; set; }
        public decimal UnitsSold { get; set; }  
        public decimal ManufacturingPrice { get; set; }
        public decimal SalePrice { get; set; }
       [DataType(DataType.Date)]
        public DateTime Date { get; set; }
    }
}
