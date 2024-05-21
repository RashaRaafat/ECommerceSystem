using ECommerce.ApplicationCore.Interfaces;
using ECommerce.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Web.User.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        public ProductsController(IProductService productService, ICategoryService categoryService)
        {
            _productService = productService;
            _categoryService = categoryService;
        }
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Index(int? Categoryid)
        {
            var products = await _productService.GetAllAsync(Categoryid);
            ViewBag.Categories = await _categoryService.GetAllAsync();
            return View(products);
        }

    }
}
