using Ecommerce.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Web.Helpers
{
    public static class ViewHelper
    {
        public static decimal GetProductPrice(decimal UnitPrice, decimal? AvailableDiscount)
        {
            AvailableDiscount = AvailableDiscount ?? 0;
            return (UnitPrice - ((UnitPrice * AvailableDiscount.Value) / 100));
        }
    }
}
