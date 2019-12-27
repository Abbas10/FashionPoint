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
    public interface ICategoryRepository
    {
        /// <summary>
        /// Get Category list
        /// </summary>
        /// <returns></returns>
        Task<List<Category>> GetCategoriesAsync(PaginationFilter paginationFilter);

        /// <summary>
        /// Get Category By Id
        /// </summary>
        /// <param name="id">Category Id</param>
        /// <returns>Category Request</returns>
        Task<Category> GetCategoryById(int id);

        /// <summary>
        /// Create New record of Category
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        Task<bool> CreateCategoryAsync(Category category);

        /// <summary>
        /// Update Category
        /// </summary>
        /// <param name="category">Category</param>
        /// <returns>bool</returns>
        Task<bool> UpdateCategoryAsync(Category category);

        /// <summary>
        /// Delete Category By It's Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> DeleteCategoryAsync(int id);
    }
}
