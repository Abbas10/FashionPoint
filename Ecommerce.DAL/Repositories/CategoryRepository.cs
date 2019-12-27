using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;
using Ecommerce.DAL.DataModels;
using Ecommerce.Model;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.DAL.Repositories
{
    /// <summary>
    /// 
    /// </summary>
    public class CategoryRepository : ICategoryRepository
    {
        #region Declaration
        private readonly EcommerceDbContext _context;
        #endregion

        #region Constructor
        public CategoryRepository(EcommerceDbContext dbContext)
        {
            _context = dbContext;
        }
        #endregion

        #region Methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="paginationFilter"></param>
        /// <returns></returns>
        public async Task<List<Category>> GetCategoriesAsync(PaginationFilter paginationFilter)
        {
            var query = _context.Categories.AsQueryable();

            if (paginationFilter == null)
            {
                return await query.ToListAsync();
            }
            else
            {
                var skip = (paginationFilter.PageNumber - 1) * paginationFilter.PageSize;
                return await query.Skip(skip).Take(paginationFilter.PageSize).ToListAsync();
            }
        }

        /// <summary>
        /// Get Category By Id
        /// </summary>
        /// <param name="id">Category Id</param>
        /// <returns>Category</returns>
        public Task<Category> GetCategoryById(int id)
        {
            return _context.Categories.Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        /// <summary>
        /// Create New record of Category
        /// </summary>
        /// <param name="category">Category object</param>
        /// <returns>bool:true/false</returns>
        public async Task<bool> CreateCategoryAsync(Category category)
        {
            await _context.Categories.AddAsync(category);
            var created = await _context.SaveChangesAsync();
            return created > 0;
        }
        
        /// <summary>
        /// Update Category
        /// </summary>
        /// <param name="category">category</param>
        /// <returns></returns>
        public async Task<bool> UpdateCategoryAsync(Category category)
        {
            _context.Categories.Update(category);
            var updated = await _context.SaveChangesAsync();
            return updated > 0;
        }

        /// <summary>
        /// Delete Category By It's Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> DeleteCategoryAsync(int id)
        {
            var category = await GetCategoryById(id);

            if (category == null)
                return false;

            _context.Categories.Remove(category);
            var deleted = await _context.SaveChangesAsync();
            return deleted > 0;
        }
        #endregion

        #region Helper Methods
        #endregion

    }
}
