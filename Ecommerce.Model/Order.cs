using System;
using System.Collections.Generic;
using System.Text;

namespace Ecommerce.Model
{
    public class CustomerOrderedProductRequest
    {
        public int ProductId { get; set; }
        public short Quantity { get; set; }

    }
    public class OrderRequest : BaseModel
    {
        /// <summary>
        /// Order Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Order No (for customer perspective)
        /// </summary>
        public string OrderNo { get; set; } = Guid.NewGuid().ToString();
        
        /// <summary>
        /// Order Date (default: Today's Date)
        /// </summary>
        public DateTime OrderDate { get; set; } = DateTime.Now;
        
        /// <summary>
        /// Total discount of the order
        /// </summary>
        public decimal TotalDiscount { get; set; }
        
        /// <summary>
        /// Total Amount after Discount
        /// </summary>
        public decimal TotalAmount { get; set; }

        /// <summary>
        /// Order Status
        /// </summary>
        public OrderStatus Status { get; set; } = OrderStatus.Received;
        
        /// <summary>
        /// Order Details
        /// </summary>
        public List<OrderDetailRequest> OrderDetails { get; set; }
    }
    public class OrderDetailRequest : BaseModel
    {
        /// <summary>
        /// Order Id
        /// </summary>
        public int OrderId { get; set; }
        
        /// <summary>
        /// Product Id
        /// </summary>
        public int ProductId { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public string ProductName { get; set; }
        
        /// <summary>
        /// Category Id
        /// </summary>
        public int CategoryId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string CategoryName { get; set; }
        
        /// <summary>
        /// Unit Id
        /// </summary>
        public int UnitId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string UnitName { get; set; }
        
        /// <summary>
        /// Quantity
        /// </summary>
        public short Quantity { get; set; }

        /// <summary>
        /// Unit Price
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal TotalDiscount { get; set; } = 0;
        /// <summary>
        /// Total Price = Price * Quantity
        /// </summary>
        public decimal TotalPrice { get; set; }
        
        /// <summary>
        /// Order Status
        /// </summary>
        public OrderStatus Status { get; set; }

    }
    public class UpdateOrderRequest 
    { 
        public OrderStatus OrderStatus { get; set; }
    }
    public enum OrderStatus
    {
        /// <summary>
        /// 
        /// </summary>
        Received = 1,
        /// <summary>
        /// 
        /// </summary>
        Packaging,
        /// <summary>
        /// 
        /// </summary>
        Completed,
        /// <summary>
        /// 
        /// </summary>
        Shipped,
        /// <summary>
        /// 
        /// </summary>
        Delivered,
        /// <summary>
        /// 
        /// </summary>
        Cancelled,
        /// <summary>
        /// 
        /// </summary>
        Returned
    }
    public class OrderFilter
    {
        /// <summary>
        /// UserId of Customer 
        /// </summary>
        public string CustomerId { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public string RetailerId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? OrderDateFrom { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public DateTime? OrderDateTo { get; set; }

        public OrderStatus? status { get; set; }
    }
}
