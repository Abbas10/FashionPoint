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
    public class OrderService : IOrderService
    {
        #region Declaration
        private readonly IProductService _productService;
        private readonly IOrderRepository _repository;
        private readonly IMapper _mapper;
        #endregion

        #region Constructor 
        public OrderService(IOrderRepository repository, IProductService productService, IMapper mapper)
        {
            _repository = repository;
            _productService = productService;
            _mapper = mapper;
        }
        #endregion


        #region Methods
        /// <summary>
        /// Get Order list
        /// </summary>
        /// <param name="paginationFilter"></param>
        /// <returns></returns>
        public async Task<List<OrderRequest>> GetOrdersAsync(OrderFilter orderFilter, PaginationFilter paginationFilter)
        {
            var data = await _repository.GetOrdersAsync(orderFilter, paginationFilter);
            return data.Select(x => new OrderRequest
            {
                Id = x.Id,
                OrderNo = x.OrderNo,
                OrderDate = x.OrderDate,
                TotalDiscount = x.TotalDiscount,
                TotalAmount = x.TotalAmount,
                Status = (OrderStatus)x.OrderDetails.OrderBy(x=> x.Status).First().Status,
            }).ToList();

            //return _mapper.Map<List<OrderRequest>>(data);
        }

        /// <summary>
        /// Get Order By Id
        /// </summary>
        /// <param name="id">Order Id</param>
        /// <returns>Order Request</returns>
        public async Task<OrderRequest> GetOrderById(int id, OrderFilter orderFilter = null)
        {
            var data = await _repository.GetOrderById(id);
            return new OrderRequest
            {
                Id = data.Id,
                OrderNo = data.OrderNo,
                OrderDate = data.OrderDate,
                TotalDiscount = data.TotalDiscount,
                TotalAmount = data.TotalAmount,
                Status = (OrderStatus)data.Status,
                CustomerDetail = new ApplicationUserRequest
                {
                    UserName = data.CreatedByUser.UserName,
                    Email = data.CreatedByUser.Email,
                    ContactNo = data.CreatedByUser.ContactNo,
                    Address = string.Format("{0} {1} {2} {3}", data.CreatedByUser.AddressLine1, data.CreatedByUser.AddressLine2, data.CreatedByUser.City, data.CreatedByUser.State)
                },
                OrderDetails = data.OrderDetails.Select(x=> new OrderDetailRequest 
                { 
                    OrderId = x.OrderId,
                    ProductId = x.Id,
                    ProductName = x.Product.ProductName,
                    CategoryId = x.CategoryId,
                    CategoryName = x.Category.CategoryName,
                    UnitId = x.UnitId,
                    UnitName = x.Unit.UnitName,
                    Quantity = x.Quantity,
                    Price = x.Price,
                    TotalDiscount = x.TotalDiscount,
                    TotalPrice = x.TotalPrice,
                    Status = (OrderStatus)x.Status,
                    CreatedBy = data.CreatedBy,
                    CreatedDate = data.OrderDate
                }).ToList()
            };

            //return _mapper.Map<OrderRequest>(data, x=> x.);
        }

        /// <summary>
        /// Create Order
        /// </summary>
        /// <param name="orderedItems"></param>
        /// <returns></returns>
        public async Task<bool> CreateOrderAsync(List<ShoppingCartRequest> orderedItems, string customerId)
        {
            var orderProducts = await _productService.GetProductsAsync(new ProductFilter
            {
                ProductIds = orderedItems.Select(x => x.ProductId).ToArray()
            }, null);

            var orderDetail = orderProducts.Select(x => new OrderDetail
            {
                CategoryId = x.CategoryId,
                ProductId = x.Id,
                UnitId = x.UnitId,
                Quantity = orderedItems.First(y => y.ProductId == x.Id).Quantity,
                Status = (short)x.Status,
                Price = x.UnitPrice,
                TotalDiscount = ((x.UnitPrice * x.AvailableDiscount.Value) / 100) * orderedItems.First(y => y.ProductId == x.Id).Quantity,
                TotalPrice = (x.UnitPrice - ((x.UnitPrice * x.AvailableDiscount.Value) / 100)) * orderedItems.First(y => y.ProductId == x.Id).Quantity,
                
            }).ToList();

            #region Total Price Calculation
            /*----------------------------------------------------------------------------------------------------------------------------- x
             *      Product     UnitPrice       Discount(%)      Quantity       Discount    TotalAmount     Explanation                     x
             * ---------------------------------------------------------------------------------------------------------------------------- x
             *                                                                                                                              x
             *      P1          1350            10                  5           135         6075            5*(1350-((1350*10)/100))        x
             *      P2          1700            10                  2           170         3060            2*(1700-((1700*10)/100))        x
             *                                                               ----------------                                               x
             *                                                                  9135                                                        x
             *******************************************************************************************************************************/
            #endregion

            return await _repository.CreateOrderAsync(new Order
            {
                OrderNo = Guid.NewGuid().ToString(),
                OrderDate = DateTime.Now,
                TotalDiscount = orderDetail.Sum(x=> x.TotalDiscount),
                TotalAmount = orderDetail.Sum(x => x.TotalPrice),
                Status = (short)OrderStatus.Received,
                CreatedBy = customerId,
                OrderDetails = orderDetail
            }); ;
        }

        /// <summary>
        /// Update Order
        /// </summary>
        /// <param name="orderId">Order Id</param>
        /// <param name="orderStatus">order status</param>
        /// <param name="retailerId">Retailer Id</param>
        /// <returns></returns>
        public async Task<bool> UpdateOrderAsync(int orderId, OrderStatus orderStatus, string retailerId)
        {
            var order = await _repository.GetOrderById(orderId, new OrderFilter { RetailerId = retailerId });

            if (order == null) return false;

            //order.Status = (short)orderStatus;
            //order.ModifiedBy = retailerId;
            order.ModifiedDate = DateTime.Now;
            order.OrderDetails = order.OrderDetails.Where(x => x.Product.CreatedBy == retailerId).ToList();
            order.OrderDetails.ForEach(x => 
            {
                x.Status = (short)orderStatus;
                x.ModifiedBy = retailerId;
                x.ModifiedDate = DateTime.Now;
            });
            return await _repository.UpdateOrderAsync(order);
        }

        /// <summary>
        /// Delete Order
        /// </summary>
        /// <param name="id"></param>
        /// <param name="orderFilter"></param>
        /// <returns></returns>
        public async Task<bool> DeleteOrderAsync(int id, OrderFilter orderFilter = null)
        {
            return await _repository.DeleteOrderAsync(id, orderFilter);
        }

        #endregion

        #region Helper Methods
        #endregion
    }
}
