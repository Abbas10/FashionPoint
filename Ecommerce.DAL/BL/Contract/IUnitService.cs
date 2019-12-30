using Ecommerce.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.DAL.BL
{
    public interface IUnitService
    {
        /// <summary>
        /// Get unit list
        /// </summary>
        /// <param name="paginationFilter"></param>
        /// <returns></returns>
        Task<List<UnitRequest>> GetUnitsAsync(PaginationFilter paginationFilter);

        /// <summary>
        /// Get Unit By Id
        /// </summary> 
        /// <param name="id">Unit Id</param>
        /// <returns>Unit Request</returns>
        Task<UnitRequest> GetUnitById(int id);

        /// <summary>
        /// Create New record of Unit
        /// </summary>
        /// <param name="unit">Unit</param>
        /// <returns></returns>
        Task<bool> CreateUnitAsync(UnitRequest unit);

        /// <summary>
        /// Update Unit
        /// </summary>
        /// <param name="unit">Unit</param>
        /// <returns>bool</returns>
        Task<bool> UpdateUnitAsync(UnitRequest unit);

        /// <summary>
        /// Delete Unit By It's Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> DeleteUnitAsync(int id);
    }
}
