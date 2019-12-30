using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Ecommerce.DAL.DataModels
{
    public class ShoppingCart
    {
        /// <summary>
        /// 
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int ProductId {get;set;}

        /// <summary>
        /// 
        /// </summary>
        [ForeignKey(nameof(ProductId))] 
        public Product Product { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string CreatedBy { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [ForeignKey(nameof(CreatedBy))]
        public ApplicationUser CreatedByUser { get; set; }

    }
}
