using Ecommerce.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.DAL.BL
{
    public interface IProductService
    {
        /// <summary>
        /// Get Product list
        /// </summary>
        /// <param name="paginationFilter"></param>
        /// <returns></returns>
        Task<List<ProductRequest>> GetProductsAsync(ProductFilter productFilter, PaginationFilter paginationFilter);

        /// <summary>
        /// Get Product By Id
        /// </summary>
        /// <param name="id">Product Id</param>
        /// <returns>Product Request</returns>
        Task<ProductRequest> GetProductById(int id, ProductFilter productFilter = null);

        /// <summary>
        /// Create New record of Product
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        Task<bool> CreateProductAsync(ProductRequest product);

        /// <summary>
        /// Update Product
        /// </summary>
        /// <param name="product">Product</param>
        /// <returns>bool</returns>
        Task<bool> UpdateProductAsync(ProductRequest product, ProductFilter productFilter = null);

        /// <summary>
        /// Delete Product By It's Id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="productFilter"></param>
        /// <returns></returns>
        Task<bool> DeleteProductAsync(int id, ProductFilter productFilter = null);
    }
}
