using System;
using System.Collections.Generic;
using System.Text;

namespace Ecommerce.Model
{
    public class ServiceDataWrapper<T>
    {
        public short ErrorCode { get; set; }
        public IEnumerable<string> Error { get; set; }
        public T value { get; set; }

    }
}
