using Ecommerce.DAL.BL;
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
    public class CartService : ICartService
    {
        #region Declaration
        private readonly ICartRepository _repository;
        #endregion

        #region Constructor
        public CartService(ICartRepository repository)
        {
            _repository = repository;
        }
        #endregion
         
        #region Methods
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<List<ShoppingCartItem>> GetCartItemsAsyc(string id)
        {
            var items = await _repository.GetCartItemsAsyc(id);
            return items.Select(x => new ShoppingCartItem
                    {
                        ProductId = x.ProductId,
                        ProductName = x.Product.ProductName,
                        Photo = x.Product.Photo,
                        Discount = x.Product.AvailableDiscount,
                        UnitPrice = x.Product.UnitPrice,
                        Quantity = x.Quantity
                    }).ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public async Task<bool> AddItemInCart(string userId, ShoppingCartRequest item)
        {
            var _item = new ShoppingCart
            {
                CreatedBy = userId,
                ProductId = item.ProductId,
                Quantity = item.Quantity
            };

            return await _repository.AddItemInCart(_item);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<bool> RemoveItemFromCart(int id, string userId)
        {
            return await _repository.RemoveItemFromCart(id, userId);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public async Task<bool> UpdateItemInCart(string userId, ShoppingCartRequest item)
        {
            return await _repository.UpdateItemInCart(new ShoppingCart 
            { 
                CreatedBy = userId,
                ProductId = item.ProductId,
                Quantity = item.Quantity,
            });
        }
        #endregion
    }
}
