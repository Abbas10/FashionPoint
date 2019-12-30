using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Ecommerce.DAL.DataModels
{
    public class Unit
    {
        /// <summary>
        /// 
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Required]
        [StringLength(150)]
        public string UnitName { get; set; }

        /// <summary>
        /// 
        /// </summary> 
        public bool IsActive { get; set; } = true;

        /// <summary>
        /// 
        /// </summary>
        public string CreatedBy { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [ForeignKey(nameof(CreatedBy))]
        public ApplicationUser CreatedByUser { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        /// <summary>
        /// 
        /// </summary>
        public string ModifiedBy { get; set; }
        [ForeignKey(nameof(ModifiedBy))]
        public ApplicationUser ModifiedByUser { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? ModifiedDate { get; set; }
    }
}
