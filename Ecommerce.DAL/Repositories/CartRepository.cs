using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Ecommerce.DAL.DataModels;
using Ecommerce.Model;

namespace Ecommerce.DAL.Repositories
{
    public class CartRepository : ICartRepository
    {
        #region Declaration
        private readonly EcommerceDbContext _context;
        #endregion

        #region Constructor
        public CartRepository(EcommerceDbContext context)
        {
            _context = context;
        }
        #endregion

        #region Methods
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<List<ShoppingCart>> GetCartItemsAsyc(string id)
        {
            var query =  _context.ShoppingCarts.Include(x=> x.Product).AsQueryable();
            return await query.Where(x => x.CreatedBy == id).ToListAsync();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public async Task<bool> AddItemInCart(ShoppingCart item)
        {
            var existItem = await _context.ShoppingCarts.FirstOrDefaultAsync(x => x.ProductId == item.ProductId && x.CreatedBy == item.CreatedBy);
            if(existItem != null) 
            {
                existItem.Quantity = existItem.Quantity + item.Quantity;
                _context.ShoppingCarts.Update(existItem);
                var updated = await _context.SaveChangesAsync();
                return updated > 0;
            }
            else
            {
                await _context.ShoppingCarts.AddAsync(item);
                var created = await _context.SaveChangesAsync();
                return created > 0;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<bool> RemoveItemFromCart(int id, string userId)
        {
            var item = await _context.ShoppingCarts.FirstOrDefaultAsync(x => x.Id == id && x.CreatedBy == userId);
            if(item != null) 
            { 
                _context.Remove(item);
                var deleted = await _context.SaveChangesAsync();
                return deleted > 0;
            }
            return false;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        public async Task<bool> UpdateItemInCart(ShoppingCart item)
        {
            var _item = await _context.ShoppingCarts.FirstOrDefaultAsync(x => x.ProductId == item.ProductId && x.CreatedBy == item.CreatedBy);
            if (item != null) {
                _item.Quantity = item.Quantity;
                _context.ShoppingCarts.Update(_item);
                var updated = await _context.SaveChangesAsync();
                return updated > 0;
            }
            return false;
        }
        #endregion
    }
}
