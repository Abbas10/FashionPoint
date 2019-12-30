using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ecommerce.Model
{
    public class UnitRequest : BaseModel
    {
        /// <summary>
        /// Unique Id
        /// </summary> 
        public int Id { get; set; }

        /// <summary>
        /// Unit Name
        /// </summary>
        [Required]
        public string UnitName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool IsActive { get; set; } = true;

    }
}
