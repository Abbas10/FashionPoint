using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Ecommerce.DAL.DataModels;
using Ecommerce.DAL.Repositories;
using Ecommerce.Model;

namespace Ecommerce.DAL.BL
{
    /// <summary>
    /// 
    /// </summary>
    public class CategoryService : ICategoryService
    {
        #region Declaration
        private readonly ICategoryRepository _repository;
        private readonly IMapper _mapper;
        #endregion

        #region Constructor 
        public CategoryService(ICategoryRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        #endregion


        #region Methods
        /// <summary>
        /// Get Category list
        /// </summary>
        /// <param name="paginationFilter"></param>
        /// <returns></returns>
        public async Task<List<CategoryRequest>> GetCategoriesAsync(PaginationFilter paginationFilter)
        {
            var data = await _repository.GetCategoriesAsync(paginationFilter);
            return data.Select(x => new CategoryRequest
            {
                Id = x.Id,
                CategoryName = x.CategoryName
            }).ToList();

            //return _mapper.Map<List<CategoryRequest>>(data);
        }

        /// <summary>
        /// Get Category By Id
        /// </summary>
        /// <param name="id">Category Id</param>
        /// <returns>Category Request</returns>
        public async Task<CategoryRequest> GetCategoryById(int id)
        {
            var data = await _repository.GetCategoryById(id);
            return new CategoryRequest
            {
                Id = data.Id,
                CategoryName = data.CategoryName,
                IsActive = data.IsActive
            };

            //return _mapper.Map<CategoryRequest>(data, x=> x.);
        }

        /// <summary>
        /// Create Category
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        public async Task<bool> CreateCategoryAsync(CategoryRequest category)
        {
            return await _repository.CreateCategoryAsync(new Category
                                                    {
                                                        CategoryName = category.CategoryName,
                                                        IsActive = category.IsActive,
                                                        CreatedBy = category.CreatedBy,
                                                    });
        }

        /// <summary>
        /// Update Category
        /// </summary>
        /// <param name="category">category</param>
        /// <returns></returns>
        public async Task<bool> UpdateCategoryAsync(CategoryRequest category)
        {
            var data = await _repository.GetCategoryById(category.Id);
            data.CategoryName = category.CategoryName;
            data.ModifiedBy = category.ModifiedBy;
            data.ModifiedDate = DateTime.Now;

            return await _repository.UpdateCategoryAsync(data);
        }

        /// <summary>
        /// Delete Category
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> DeleteCategoryAsync(int id)
        {
            return await _repository.DeleteCategoryAsync(id);
        }

        #endregion

        #region Helper Methods
        #endregion
    }
}
