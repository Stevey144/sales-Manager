using SalesDashboard.Models;
using System.Linq;

namespace SalesDashboard.Services
{
    public class SalesService
    {
        public List<Sale> GetAllSales() => CsvHelperService.ReadCsv();

        public Sale GetSaleById(int id) => GetAllSales().FirstOrDefault(s => s.Id == id);

        public void AddSale(Sale sale)
        {
            var sales = GetAllSales();
            sale.Id = sales.Any() ? sales.Max(s => s.Id) + 1 : 1;  
            sales.Add(sale);
            CsvHelperService.WriteCsv(sales);
        }

        public void UpdateSale(Sale updatedSale)
        {
            var sales = GetAllSales();
            var sale = sales.FirstOrDefault(s => s.Id == updatedSale.Id);
            if (sale == null) return;

            sale.Segment = updatedSale.Segment;
            sale.Country = updatedSale.Country;
            sale.Product = updatedSale.Product;
            sale.DiscountBand = updatedSale.DiscountBand;
            sale.UnitsSold = updatedSale.UnitsSold;
            sale.ManufacturingPrice = updatedSale.ManufacturingPrice;
            sale.SalePrice = updatedSale.SalePrice;
            sale.Date = updatedSale.Date;

            CsvHelperService.WriteCsv(sales);
        }

        public void DeleteSale(int id)
        {
            var sales = GetAllSales().Where(s => s.Id != id).ToList();

            CsvHelperService.WriteCsv(sales);
        }

    }
}
