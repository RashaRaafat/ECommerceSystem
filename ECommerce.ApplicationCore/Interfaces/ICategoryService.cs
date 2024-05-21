using System.Collections.Generic;
using System.Threading.Tasks;
using ECommerce.ApplicationCore.Models;

    namespace ECommerce.ApplicationCore.Interfaces
    {
        public interface ICategoryService
    {
            Task<Category> GetByIdAsync(int id);
            Task<List<Category>> GetAllAsync();
            Task AddAsync(Category Category);
            Task UpdateAsync(Category Category);
            Task DeleteAsync(Category Category);
        }
    }


