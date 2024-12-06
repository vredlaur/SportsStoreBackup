using Microsoft.AspNetCore.Mvc;
using SportsStore.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;

namespace SportsStore.Controllers
{
    public class HomeController : Controller
    {
        private readonly IStoreRepository _repository;
        private readonly ILogger<HomeController> _logger;

        public HomeController(IStoreRepository repository, ILogger<HomeController> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public IActionResult Index()
        {
            try
            {
                var products = _repository?.Products?.ToList();

                if (products == null || !products.Any())
                {
                    _logger.LogError("No products found in the repository.");
                    return View("Error", new { message = "No products found." });
                }

                return View(products);
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred: {ex.Message} - Stack Trace: {ex.StackTrace}");
                return View("Error", new { message = "Something went wrong while loading the products." });
            }
        }

        public IActionResult TestProducts()
        {
            try
            {
                var products = _repository?.Products?.ToList();
                if (products == null || !products.Any())
                {
                    return Content("No products found in the database.");
                }

                return Content($"Found {products.Count} products. Example: {products.FirstOrDefault()?.Name}");
            }
            catch (Exception ex)
            {
                return Content($"Error: {ex.Message} - Stack Trace: {ex.StackTrace}");
            }
        }
    }
}
