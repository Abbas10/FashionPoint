using System;
using System.Collections.Generic;
using System.Text;

namespace Ecommerce.Model
{
    public class ShoppingCartRequest
    {
        /// <summary>
        /// 
        /// </summary>
        public int ProductId { get; set; }
        /// <summary> 
        /// 
        /// </summary>
        public short Quantity { get; set; }
    }
    public class ShoppingCartItem
    {
        /// <summary>
        /// 
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int ProductId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ProductName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Photo { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal UnitPrice { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal? Discount { get; set; } 

        /// <summary>
        /// 
        /// </summary>
        public string CreatedBy { get; set; }

    }
}
