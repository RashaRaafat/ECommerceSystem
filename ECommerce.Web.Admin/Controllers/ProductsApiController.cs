using ECommerce.ApplicationCore.Interfaces;
using ECommerce.ApplicationCore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Text;
using ECommerce.Web.User.Helpers;
using Microsoft.Data.SqlClient.Server;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace ECommerce.Web.Admin.Controllers
{
    //[Authorize(Policy = "AdminOnly")]
    //[Authorize(Roles ="Admin")]
    [Authorize]

    [ApiController]
    [Route("api/[controller]")]
    public class ProductsApiController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsApiController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        [Route("GetAllProducts")]

        public async Task<IActionResult> GetAllProducts()
        {
            var products = await _productService.GetAllAsync();
            return products.SerializeObject();
        }

        [HttpGet]
        [Route("GetProduct/{id}")]

        public async Task<IActionResult> GetProductById(int id)
        {
            Product product = await _productService.GetByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return product.SerializeObject();
        }


        [HttpGet]
        [Route("FilterProductBycategory/{id}")]

        public async Task<IActionResult> FilterProductBycategory(int id)
        {
            var products = await _productService.GetAllAsync(id);
            if (products == null)
            {
                return NotFound();
            }
            return products.SerializeObject();
        }
        //[HttpPost]
        //[Route("AddProduct")]
        //public async Task<IActionResult> CreateProduct(Product product)
        //{
        //    await _productService.AddAsync(product);
        //    return CreatedAtAction(nameof(GetProductById), new { id = product.Id }, product);
        //}
        [HttpPost]
        [Route("AddProduct")]
        public async Task<IActionResult> CreateProduct([FromForm] ProductViewModel formData)
        {
            if (formData.Image != null && formData.Image.Length > 0)
            {
                var imagePath = Path.Combine("wwwroot", "images", formData.Image.FileName);
                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    await formData.Image.CopyToAsync(stream);
                }
                Product product = new Product
                {
                    Name = formData.Name,
                    Description = formData.Description,
                    Price = formData.Price,
                    Quantity = formData.Quantity,
                    ImagePath = "~/images/"+ formData.Image.FileName,
                    CategoryId = formData.CategoryId
                };
                await _productService.AddAsync(product);
            }

            return Ok(new { message = "Product added successfully" });
       
        }

        [HttpPost]
        [Route("updateProduct")]

        public async Task<IActionResult> UpdateProduct([FromForm] ProductViewModel formData)
        {
            Product product = await _productService.GetByIdAsync(formData.Id);
            if (product == null)
            {
                return NotFound();
            }
            if (formData.Image != null && formData.Image.Length > 0)
            {
                var imagePath = Path.Combine("wwwroot", "images", formData.Image.FileName);
                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    await formData.Image.CopyToAsync(stream);
                }
                product.ImagePath = "~/images/" + formData.Image.FileName;
            }
            product.Name = formData.Name;
            product.Description = formData.Description;
            product.Price = formData.Price;
            product.Quantity = formData.Quantity;
            product.CategoryId = formData.CategoryId;

            await _productService.UpdateAsync(product);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _productService.GetByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            await _productService.DeleteAsync(product);
            return NoContent();
        }
    }
}
