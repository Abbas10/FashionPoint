using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Ecommerce.Model;


namespace Ecommerce.DAL.BL
{
    /// <summary>
    /// 
    /// </summary>
    public interface ICategoryService
    {

        /// <summary>
        /// Get category list
        /// </summary>
        /// <param name="paginationFilter"></param>
        /// <returns></returns>
        Task<List<CategoryRequest>> GetCategoriesAsync(PaginationFilter paginationFilter);

        /// <summary>
        /// Get Category By Id
        /// </summary>
        /// <param name="id">Category Id</param>
        /// <returns>Category Request</returns>
        Task<CategoryRequest> GetCategoryById(int id);

        /// <summary>
        /// Create New record of Category
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        Task<bool> CreateCategoryAsync(CategoryRequest category);
        
        /// <summary>
        /// Update Category
        /// </summary>
        /// <param name="category">Category</param>
        /// <returns>bool</returns>
        Task<bool> UpdateCategoryAsync(CategoryRequest category);

        /// <summary>
        /// Delete Category By It's Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> DeleteCategoryAsync(int id);
    }
}
