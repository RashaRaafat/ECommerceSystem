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
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace ECommerce.Web.Admin.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriesApiController : ControllerBase
    {
        private readonly ICategoryService _Categorieservice;

        public CategoriesApiController(ICategoryService Categorieservice)
        {
            _Categorieservice = Categorieservice;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllCategories()
        {
            var Categories = await _Categorieservice.GetAllAsync();
            return Categories.SerializeObject();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategoryById(int id)
        {
            var Category = await _Categorieservice.GetByIdAsync(id);
            if (Category == null)
            {
                return NotFound();
            }
            return Category.SerializeObject();
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory([FromBody] CategoryViewModel category)
        {
            try
            {
                Category newCategory = new Category() { Description = category.Description, Name = category.Name };
                await _Categorieservice.AddAsync(newCategory);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(int id, [FromBody] CategoryViewModel category)
        {
            var updatedCategory = await _Categorieservice.GetByIdAsync(id);
            updatedCategory.Description = category.Description;
            updatedCategory.Name = category.Name;
            await _Categorieservice.UpdateAsync(updatedCategory);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var Category = await _Categorieservice.GetByIdAsync(id);
            if (Category == null)
            {
                return NotFound();
            }

            await _Categorieservice.DeleteAsync(Category);
            return NoContent();
        }
    }
    public class CategoryViewModel
    {
        public string Name { get; set; }
        public string? Description { get; set; }
    }
}
