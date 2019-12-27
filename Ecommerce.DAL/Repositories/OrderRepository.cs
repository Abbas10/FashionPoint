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
    public class OrderRepository: IOrderRepository
    {
        #region Declaration
        private readonly EcommerceDbContext _context;
        #endregion

        #region Constructor
        public OrderRepository(EcommerceDbContext dbContext)
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
        public async Task<List<Order>> GetOrdersAsync(OrderFilter orderFilter, PaginationFilter paginationFilter)
        {
            var query = _context.Orders.AsQueryable();

            query = AddFiltersOnQuery(orderFilter, query);

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
        /// Get Order By Id
        /// </summary>
        /// <param name="id">Order Id</param>
        /// <returns>Order</returns>
        public async Task<Order> GetOrderById(int id, OrderFilter orderFilter = null)
        {
            var query = _context.Orders.AsQueryable();
            query = AddFiltersOnQuery(orderFilter, query);
            return await _context.Orders.Include(x => x.OrderDetails)   
                                            .ThenInclude(orderDtails=>  orderDtails.Product)
                                        .Include(x => x.OrderDetails)
                                            .ThenInclude(y=> y.Category)
                                        .Include(x => x.OrderDetails)
                                            .ThenInclude(y => y.Unit)
                                    .Where(x => x.Id == id)
                                    .FirstOrDefaultAsync();
        }

        /// <summary>
        /// Create New record of Order
        /// </summary>
        /// <param name="Order">Order object</param>
        /// <returns>bool:true/false</returns>
        public async Task<bool> CreateOrderAsync(Order order)
        {
            await _context.Orders.AddAsync(order);
            var created = await _context.SaveChangesAsync();

            ///await AddNewOrderDetail(order);

            return created > 0;
        }

        /// <summary>
        /// Update Order
        /// </summary>
        /// <param name="Order">Order</param>
        /// <returns></returns>
        public async Task<bool> UpdateOrderAsync(Order order)
        {
            _context.Orders.Update(order);
            var updated = await _context.SaveChangesAsync();
            return updated > 0;
        }

        /// <summary>
        /// Delete Order By It's Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> DeleteOrderAsync(int id, OrderFilter orderFilter = null)
        {
            var order = await GetOrderById(id, orderFilter);

            if (order == null)
                return false;

            _context.Orders.Remove(order);
            var deleted = await _context.SaveChangesAsync();
            return deleted > 0;
        }
        #endregion

        #region Helper Methods
        /// <summary>
        /// 
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="query"></param>
        /// <returns></returns>
        private static IQueryable<Order> AddFiltersOnQuery(OrderFilter filter, IQueryable<Order> query)
        {
            if (filter == null) return query;
            query = (!string.IsNullOrEmpty(filter?.CustomerId)) ? query.Where(x => x.CreatedBy == filter.CustomerId) : query;
            query = (filter.OrderDateFrom != null && filter.OrderDateTo != null) ? query.Where(x => x.OrderDate >= filter.OrderDateFrom && x.OrderDate <= filter.OrderDateTo) : query;
            query = (filter.status != null) ? query.Where(x => x.Status == ((short)filter.status)) : query;
            query = (!string.IsNullOrEmpty(filter?.RetailerId)) ? query.Where(x => x.OrderDetails.Any(y => y.Product.CreatedBy == filter.RetailerId)) : query;

            return query;
        }
        #endregion
    }
}
