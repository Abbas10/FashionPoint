using Ecommerce.DAL.DataModels;
using Ecommerce.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.DAL.Repositories
{
    public interface ICartRepository
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">Customer Id</param>
        /// <returns></returns>
        Task<List<ShoppingCart>> GetCartItemsAsyc(string id);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        Task<bool> AddItemInCart(ShoppingCart item); 

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        Task<bool> UpdateItemInCart(ShoppingCart item);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> RemoveItemFromCart(int id, string userId);

    }
}
