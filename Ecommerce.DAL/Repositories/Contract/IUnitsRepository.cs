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
    public interface IUnitsRepository
    {
        /// <summary>
        /// Get Unit list
        /// </summary>
        /// <returns></returns>
        Task<List<Unit>> GetUnitsAsync(PaginationFilter paginationFilter);

        /// <summary>
        /// Get Unit By Id
        /// </summary>
        /// <param name="id">Unit Id</param>
        /// <returns>Unit</returns>
        Task<Unit> GetUnitById(int id);

        /// <summary>
        /// Create New record of Unit
        /// </summary>
        /// <param name="unit"></param>
        /// <returns>true/false</returns>
        Task<bool> CreateUnitAsync(Unit unit);

        /// <summary>
        /// Update Unit
        /// </summary>
        /// <param name="unit">Unit</param>
        /// <returns>bool</returns>
        Task<bool> UpdateUnitAsync(Unit unit);

        /// <summary>
        /// Delete Unit By It's Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> DeleteUnitAsync(int id);
    }
}
