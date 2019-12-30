using AutoMapper;
using Ecommerce.DAL.DataModels;
using Ecommerce.DAL.Repositories;
using Ecommerce.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.DAL.BL
{
    /// <summary>
    /// 
    /// </summary>
    public class UnitService : IUnitService
    {
        #region Declaration
        private readonly IUnitsRepository _repository;
        private readonly IMapper _mapper;
        #endregion

        #region Constructor 
        public UnitService(IUnitsRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        #endregion


        #region Methods

        /// <summary>
        /// Get Unit list
        /// </summary>
        /// <param name="paginationFilter"></param>
        /// <returns></returns>
        public async Task<List<UnitRequest>> GetUnitsAsync(PaginationFilter paginationFilter)
        {
            var data = await _repository.GetUnitsAsync(paginationFilter);
            return data.Select(x => new UnitRequest
            {
                Id = x.Id,
                UnitName = x.UnitName
            }).ToList();

            //return _mapper.Map<List<UnitRequest>>(data);
        }

        /// <summary>
        /// Get Unit By Id
        /// </summary>
        /// <param name="id">Unit Id</param>
        /// <returns>Unit Request</returns>
        public async Task<UnitRequest> GetUnitById(int id)
        {
            var data = await _repository.GetUnitById(id);
            return new UnitRequest
            {
                Id = data.Id,
                UnitName = data.UnitName,
                IsActive = data.IsActive
            };

            //return _mapper.Map<UnitRequest>(data, x=> x.);
        }

        /// <summary>
        /// Create unit
        /// </summary>
        /// <param name="unit"></param>
        /// <returns></returns>
        public async Task<bool> CreateUnitAsync(UnitRequest unit)
        {
            return await _repository.CreateUnitAsync(new Unit
            {
                UnitName = unit.UnitName,
                IsActive = unit.IsActive,
                CreatedBy = unit.CreatedBy,
            });
        }

        /// <summary>
        /// Update unit
        /// </summary>
        /// <param name="unit">unit</param>
        /// <returns></returns>
        public async Task<bool> UpdateUnitAsync(UnitRequest unit)
        {
            var data = await _repository.GetUnitById(unit.Id);
            data.UnitName = unit.UnitName;
            data.ModifiedBy = unit.ModifiedBy;
            data.ModifiedDate = DateTime.Now;

            return await _repository.UpdateUnitAsync(data);
        }

        /// <summary>
        /// Delete Unit
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> DeleteUnitAsync(int id)
        {
            return await _repository.DeleteUnitAsync(id);
        }

        #endregion

        #region Helper Methods
        #endregion
    }
}
