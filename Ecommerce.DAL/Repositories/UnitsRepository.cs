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
    public class UnitsRepository : IUnitsRepository
    {
        #region Declaration
        private readonly EcommerceDbContext _context;
        #endregion

        #region Constructor
        public UnitsRepository(EcommerceDbContext dbContext)
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
        public async Task<List<Unit>> GetUnitsAsync(PaginationFilter paginationFilter)
        {
            var query = _context.Units.AsQueryable();

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
        /// Get Unit By Id
        /// </summary>
        /// <param name="id">Unit Id</param>
        /// <returns>Unit</returns>
        public Task<Unit> GetUnitById(int id)
        {
            return _context.Units.Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        /// <summary>
        /// Create New record of Unit
        /// </summary>
        /// <param name="unit">Unit object</param>
        /// <returns>bool:true/false</returns>
        public async Task<bool> CreateUnitAsync(Unit unit)
        {
            await _context.Units.AddAsync(unit);
            var created = await _context.SaveChangesAsync();
            return created > 0;
        }

        /// <summary>
        /// Update Unit
        /// </summary>
        /// <param name="unit">unit</param>
        /// <returns></returns>
        public async Task<bool> UpdateUnitAsync(Unit unit)
        {
            _context.Units.Update(unit);
            var updated = await _context.SaveChangesAsync();
            return updated > 0;
        }

        /// <summary>
        /// Delete Unit By It's Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> DeleteUnitAsync(int id)
        {
            var unit = await GetUnitById(id);

            if (unit == null)
                return false;

            _context.Units.Remove(unit);
            var deleted = await _context.SaveChangesAsync();
            return deleted > 0;
        }
        #endregion

        #region Helper Methods
        #endregion
    }
}
