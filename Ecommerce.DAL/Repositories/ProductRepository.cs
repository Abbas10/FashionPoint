using Ecommerce.DAL.DataModels;
using Ecommerce.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.DAL.Repositories
{
    /// <summary>
    /// 
    /// </summary>
    public class ProductRepository : IProductRepository
    {
        #region Declaration
        private readonly EcommerceDbContext _context;
        #endregion

        #region Constructor
        public ProductRepository(EcommerceDbContext dbContext)
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
        public async Task<List<Product>> GetProductsAsync(ProductFilter productFilter, PaginationFilter paginationFilter)
        {
            var query = _context.Products.AsQueryable();
            
            query = AddFiltersOnQuery(productFilter, query);

            if (paginationFilter == null)
            {
                return await query
                            .Include(x=> x.Unit)
                            .Include(x=> x.Category).ToListAsync();
            }
            else
            {
                var skip = (paginationFilter.PageNumber - 1) * paginationFilter.PageSize;
                return await query
                                .Include(x => x.Unit)
                                .Include(x => x.Category)
                                .Skip(skip).Take(paginationFilter.PageSize).ToListAsync();
            }
        }

        /// <summary>
        /// Get Product By Id
        /// </summary>
        /// <param name="id">Product Id</param>
        /// <returns>Product</returns>
        public Task<Product> GetProductById(int id, ProductFilter productFilter = null)
        {
            var query = _context.Products.AsQueryable();
            query = AddFiltersOnQuery(productFilter, query);
            return query.Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        /// <summary>
        /// Create New record of Product
        /// </summary>
        /// <param name="Product">Product object</param>
        /// <returns>bool:true/false</returns>
        public async Task<bool> CreateProductAsync(Product product)
        {
            await _context.Products.AddAsync(product);
            var created = await _context.SaveChangesAsync();
            return created > 0;
        }

        /// <summary>
        /// Update Product
        /// </summary>
        /// <param name="Product">Product</param>
        /// <returns></returns>
        public async Task<bool> UpdateProductAsync(Product product)
        {
            _context.Products.Update(product);
            var updated = await _context.SaveChangesAsync();
            return updated > 0;
        }

        /// <summary>
        /// Delete Product By It's Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> DeleteProductAsync(int id, ProductFilter productFilter = null)
        {
            var product = await GetProductById(id, productFilter);

            if (product == null)
                return false;

            _context.Products.Remove(product);
            var deleted = await _context.SaveChangesAsync();
            return deleted > 0;
        }
        #endregion

        #region Helper Methods
        /// <summary>
        /// 
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="queryable"></param>
        /// <returns></returns>
        private static IQueryable<Product> AddFiltersOnQuery(ProductFilter filter, IQueryable<Product> queryable)
        {
            /*
             * this filter is used to get products of perticular retailer only.
             */
            queryable = (!string.IsNullOrEmpty(filter?.CreatedBy))? queryable.Where(x => x.CreatedBy == filter.CreatedBy) : queryable;
            /*
             * this filter is used to get active products only
             */
            queryable = (filter?.IsActive != null) ? queryable.Where(x => x.IsActive == filter.IsActive) : queryable;
            /*
             * this filter is used to get product for perticular order calculate the discount and total price.
             */
            queryable = (filter?.ProductIds?.Length > 0) ? queryable.Where(x => filter.ProductIds.Any(y => y == x.Id)) : queryable;
            return queryable;
        }
        #endregion
    }
}
