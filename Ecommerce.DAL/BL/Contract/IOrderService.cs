using Ecommerce.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.DAL.BL
{
    public interface IOrderService
    {
        /// <summary>
        /// Get Order list
        /// </summary>
        /// <param name="paginationFilter"></param>
        /// <returns></returns>
        Task<List<OrderRequest>> GetOrdersAsync(OrderFilter orderFilter, PaginationFilter paginationFilter);

        /// <summary>
        /// Get Order By Id
        /// </summary>
        /// <param name="id">Order Id</param>
        /// <returns>Order Request</returns>
        Task<OrderRequest> GetOrderById(int id, OrderFilter orderFilter = null);

        /// <summary>
        /// Create New record of Order
        /// </summary>
        /// <param name="order"></param>
        /// <param name="customerId"></param>
        /// <returns></returns>
        Task<bool> CreateOrderAsync(List<CustomerOrderedProductRequest> order, string customerId);

        /// <summary>
        /// Update Order
        /// </summary>
        /// <param name="orderId">Order Id</param>
        /// <param name="orderStatus">order status</param>
        /// <param name="retailerId">Retailer Id</param>
        /// <returns></returns>
        Task<bool> UpdateOrderAsync(int orderId, OrderStatus order, string retailerId);

        /// <summary>
        /// Delete Order By It's Id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="orderFilter"></param>
        /// <returns></returns>
        Task<bool> DeleteOrderAsync(int id, OrderFilter orderFilter = null);
    }
}
