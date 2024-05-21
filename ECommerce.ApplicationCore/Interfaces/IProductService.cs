using System.Collections.Generic;
using System.Threading.Tasks;
using ECommerce.ApplicationCore.Models;

    namespace ECommerce.ApplicationCore.Interfaces
    {
        public interface IProductService
        {
            Task<Product> GetByIdAsync(int id);
            Task<List<Product>> GetAllAsync(int? Categoryid=null);
            Task AddAsync(Product product);
            Task UpdateAsync(Product product);
            Task DeleteAsync(Product product);
        }
    }


