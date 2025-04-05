using System;
using System.ComponentModel.DataAnnotations;

namespace SalesDashboard.Models
{
    public class Sale
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Segment is required.")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Segment must contain only letters.")]
        public string Segment { get; set; }

        [Required(ErrorMessage = "Country is required.")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Country must contain only letters.")]
        public string Country { get; set; }

        [Required(ErrorMessage = "Product is required.")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Product must contain only letters.")]
        public string Product { get; set; }

        [Required(ErrorMessage = "Discount Band is required.")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Discount Band must contain only letters.")]
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
