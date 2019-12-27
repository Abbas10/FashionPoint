using Ecommerce.DAL.DataModels;
using Ecommerce.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.DAL.Repositories
{
    /// <summary>
    /// 
    /// </summary>
    public interface IProductRepository
    {
        /// <summary>
        /// Get Product list
        /// </summary>
        /// <returns></returns>
        Task<List<Product>> GetProductsAsync(ProductFilter productFilter,  PaginationFilter paginationFilter);

        /// <summary>
        /// Get Product By Id
        /// </summary>
        /// <param name="id">Product Id</param>
        /// <returns>Product Request</returns>
        Task<Product> GetProductById(int id, ProductFilter productFilter = null);

        /// <summary>
        /// Create New record of Product
        /// </summary>
        /// <param name="Product"></param>
        /// <returns></returns>
        Task<bool> CreateProductAsync(Product Product);

        /// <summary>
        /// Update Product
        /// </summary>
        /// <param name="Product">Product</param>
        /// <returns>bool</returns>
        Task<bool> UpdateProductAsync(Product Product);

        /// <summary>
        /// Delete Product By It's Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> DeleteProductAsync(int id, ProductFilter productFilter = null);
    }
}
