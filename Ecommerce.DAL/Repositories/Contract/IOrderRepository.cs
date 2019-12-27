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
    public interface IOrderRepository
    {
        /// <summary>
        /// Get Order list
        /// </summary>
        /// <returns></returns>
        Task<List<Order>> GetOrdersAsync(OrderFilter orderFilter, PaginationFilter paginationFilter);

        /// <summary>
        /// Get Order By Id
        /// </summary>
        /// <param name="id">Order Id</param>
        /// <returns>Order Request</returns>
        Task<Order> GetOrderById(int id, OrderFilter orderFilter = null);

        /// <summary>
        /// Create New record of Order
        /// </summary>
        /// <param name="Order"></param>
        /// <returns></returns>
        Task<bool> CreateOrderAsync(Order order);

        /// <summary>
        /// Update Order
        /// </summary>
        /// <param name="Order">Order</param>
        /// <returns>bool</returns>
        Task<bool> UpdateOrderAsync(Order order);

        /// <summary>
        /// Delete Order By It's Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> DeleteOrderAsync(int id, OrderFilter orderFilter = null);
    }
}
