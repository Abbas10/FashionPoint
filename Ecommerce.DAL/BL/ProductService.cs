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
    public class ProductService : IProductService
    {
        #region Declaration
        private readonly IProductRepository _repository;
        private readonly IMapper _mapper;
        #endregion

        #region Constructor 
        public ProductService(IProductRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        #endregion


        #region Methods
        /// <summary>
        /// Get Product list
        /// </summary>
        /// <param name="paginationFilter"></param>
        /// <returns></returns>
        public async Task<List<ProductRequest>> GetProductsAsync(ProductFilter productFilter, PaginationFilter paginationFilter)
        {
            var data = await _repository.GetProductsAsync(productFilter, paginationFilter);
            return data.Select(x => new ProductRequest
            {
                Id = x.Id,
                ProductName = x.ProductName,
                Photo = x.Photo,
                UnitPrice = x.UnitPrice,
                AvailableDiscount = x.AvailableDiscount,
                Status = (ProductStatus)x.Status,
                CategoryId = x.CategoryId,
                CategoryName = x.Category.CategoryName,
                UnitId = x.UnitId,
                UnitName = x.Unit.UnitName,
                IsActive = x.IsActive
            }).ToList();

            //return _mapper.Map<List<ProductRequest>>(data);
        }

        /// <summary>
        /// Get Product By Id
        /// </summary>
        /// <param name="id">Product Id</param>
        /// <returns>Product Request</returns>
        public async Task<ProductRequest> GetProductById(int id, ProductFilter productFilter = null)
        {
            var data = await _repository.GetProductById(id, productFilter);
            if (data == null) return null;
            return new ProductRequest
            {
                Id = data.Id,
                ProductName = data.ProductName,
                Photo = data.Photo,
                UnitPrice = data.UnitPrice,
                AvailableDiscount = data.AvailableDiscount,
                Status = (ProductStatus)data.Status,
                CategoryId = data.CategoryId,
                UnitId = data.UnitId,
                IsActive = data.IsActive
            };

            //return _mapper.Map<ProductRequest>(data, x=> x.);
        }

        /// <summary>
        /// Create Product
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        public async Task<bool> CreateProductAsync(ProductRequest product)
        {
            return await _repository.CreateProductAsync(new Product
            {
                ProductName = product.ProductName,
                Photo = product.Photo,
                UnitPrice = product.UnitPrice,
                AvailableDiscount = product.AvailableDiscount ?? 0,
                Status = (short)product.Status,
                CategoryId = product.CategoryId,
                UnitId = product.UnitId,
                IsActive = product.IsActive,
                CreatedBy = product.CreatedBy
            });
        }

        /// <summary>
        /// Update Product
        /// </summary>
        /// <param name="product">product</param>
        /// <returns></returns>
        public async Task<bool> UpdateProductAsync(ProductRequest product, ProductFilter productFilter = null)
        {
            var data = await _repository.GetProductById(product.Id, productFilter);

            if (data == null) return false;

            data.ProductName = product.ProductName;
            data.Photo = product.Photo;
            data.UnitPrice = product.UnitPrice;
            data.AvailableDiscount = product.AvailableDiscount.Value;
            data.Status = (short)product.Status;
            data.CategoryId = product.CategoryId;
            data.UnitId = product.UnitId;
            data.IsActive = product.IsActive;
            data.ModifiedBy = product.ModifiedBy;
            data.ModifiedDate = DateTime.Now;

            return await _repository.UpdateProductAsync(data);
        }

        /// <summary>
        /// Delete Product
        /// </summary>
        /// <param name="id"></param>
        /// <param name="productFilter"></param>
        /// <returns></returns>
        public async Task<bool> DeleteProductAsync(int id, ProductFilter productFilter = null)
        {
            return await _repository.DeleteProductAsync(id, productFilter);
        }

        #endregion

        #region Helper Methods
        #endregion
    }
}
