using Microsoft.AspNetCore.Mvc;
using SalesDashboard.Models;
using SalesDashboard.Services;
using X.PagedList.Extensions;

namespace SalesDashboard.Controllers
    {
        public class SalesController : Controller
        {
            private readonly SalesService _salesService;

            public SalesController()
            {
                _salesService = new SalesService();
            }

        public IActionResult Index(string country, string product, string discountBand, int? page)
        {
            var allSales = _salesService.GetAllSales();

            ViewBag.Countries = allSales.Select(s => s.Country).Distinct().OrderBy(c => c).ToList();
            ViewBag.Products = allSales.Where(s => !string.IsNullOrWhiteSpace(s.Product))
                                         .Select(s => s.Product)
                                         .Distinct()
                                         .OrderBy(p => p)
                                         .ToList();

            ViewBag.DiscountBands = new List<string> { "None", "Low", "Medium", "High" };

            var sales = allSales.AsQueryable();  

            if (!string.IsNullOrEmpty(country))
                sales = sales.Where(s => s.Country.Equals(country, StringComparison.OrdinalIgnoreCase));

            if (!string.IsNullOrEmpty(product))
                sales = sales.Where(s => s.Product.Equals(product, StringComparison.OrdinalIgnoreCase));

            if (!string.IsNullOrEmpty(discountBand))
                sales = sales.Where(s => s.DiscountBand.Trim().Equals(discountBand.Trim(), StringComparison.OrdinalIgnoreCase));

            int pageSize = 10;
            int pageNumber = page ?? 1;

            var pagedSales = sales.ToPagedList(pageNumber, pageSize);

            ViewData["Country"] = country;
            ViewData["Product"] = product;
            ViewData["DiscountBand"] = discountBand;

            return View(pagedSales);
        }

        public IActionResult Create() => View();

        [HttpPost]
        public IActionResult Create(Sale sale)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors);
                foreach (var error in errors)
                {
                    Console.WriteLine(error.ErrorMessage);
                }
                return View(sale);
            }
            _salesService.AddSale(sale);
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            var sale = _salesService.GetSaleById(id);
            if (sale == null) return NotFound();

            ViewBag.DiscountBands = new List<string> { "None", "Low", "Medium", "High" };
            return View(sale);
        }

        [HttpPost]
        public IActionResult Edit(Sale sale)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.DiscountBands = new List<string> { "None", "Low", "Medium", "High" };
                return View(sale);
            }

            _salesService.UpdateSale(sale);
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
            {
                var sale = _salesService.GetSaleById(id);
                if (sale == null) return NotFound();
                return View(sale);
            }

            [HttpPost, ActionName("Delete")]
            public IActionResult DeleteConfirmed(int id)
            {
                _salesService.DeleteSale(id);
                return RedirectToAction("Index");
            }
        }
    }
