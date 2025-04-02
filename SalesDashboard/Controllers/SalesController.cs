    using Microsoft.AspNetCore.Mvc;
    using SalesDashboard.Models;
    using SalesDashboard.Services;
    using System.Globalization;

    namespace SalesDashboard.Controllers
    {
        public class SalesController : Controller
        {
            private readonly SalesService _salesService;

            public SalesController()
            {
                _salesService = new SalesService();
            }

       public IActionResult Index()
            {
                //CsvHelperService.ReadCsvAndPrintToConsole();
                var sales = _salesService.GetAllSales();
                if (!sales.Any())
                {
                    ViewBag.Message = "No sales records available.";
                }
                return View(sales);
            }

            public IActionResult Create() => View();

            [HttpPost]
        [HttpPost]
        public IActionResult Create(Sale sale)
        {
            if (!ModelState.IsValid)
            {
                // Add this to see validation errors
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
                return View(sale);
            }

            [HttpPost]
            public IActionResult Edit(Sale sale)
            {
                if (!ModelState.IsValid) return View(sale);
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
