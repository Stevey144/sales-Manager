using System;
using System.ComponentModel.DataAnnotations;

namespace SalesDashboard.Models
{
    public class Sale
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Segment is required.")]
        public string Segment { get; set; }

        [Required(ErrorMessage = "Country is required.")]
        public string Country { get; set; }

        [Required(ErrorMessage = "Product is required.")]
        public string Product { get; set; }

        [Required(ErrorMessage = "Discount Band is required.")]
        public string DiscountBand { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Units Sold must be at least 1.")]
        public decimal UnitsSold { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "Manufacturing Price must be greater than zero.")]
        public decimal ManufacturingPrice { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "Sale Price must be greater than zero.")]
        public decimal SalePrice { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Date is required.")]
        public DateTime Date { get; set; }
    }
}
