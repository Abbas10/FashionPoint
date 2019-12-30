using System;
using System.Collections.Generic;
using System.Text;

namespace Ecommerce.Model
{
    /// <summary>
    /// Api Routes
    /// </summary>
    public struct ApiRoutes
    {
        public const string Root = "api";
        public const string Version = "v1";
        public const string Base = Root + "/" + Version;

        /// <summary>
        /// Identity
        /// </summary>
        public struct Identity
        {
            /// <summary>
            /// Customer Registration
            /// </summary>
            public const string CustomerRegistration = Root + "/identity/customer-registration";
            
            /// <summary>
            /// Retailer Registration
            /// </summary>
            public const string RetailerRegistration = Root + "/identity/retailer-registration";
            
            /// <summary>
            /// Login
            /// </summary>
            public const string Login = Root + "/identity/login";
            
            /// <summary>
            /// Refresh
            /// </summary>
            public const string Refresh = Root + "/identity/refresh";

            /// <summary>
            /// Get All Application user
            /// </summary>
            public const string GetAll = Root + "/identity/get-all";

            /// <summary>
            /// lock-unlock
            /// </summary>
            public const string LockUnlock = Root + "/identity/lock-unlock/{0}";

            /// <summary>
            /// 
            /// </summary>
            public const string ConfirmEmail = Root + "/identity/confirm-email/{0}";
        }
        
        /// <summary>
        /// Category
        /// </summary>
        public struct Category
        {
            /// <summary>
            /// Get Category List
            /// </summary>
            public const string GetAll = Root + "/category/get-all";

            /// <summary>
            /// Get Category By it's  Id
            /// Ex. category/get/1
            /// </summary>
            public const string Get = Root + "/category/get/{0}";

            /// <summary>
            /// Create Category
            /// </summary>
            public const string CreateCategory = Root + "/category/create-category";

            /// <summary>
            /// Update Category
            /// </summary>
            public const string UpdateCategory = Root + "/category/update-category/{0}";
            
            /// <summary>
            /// Delete Category it's id
            /// </summary>
            public const string Delete = Root + "/category/delete/{0}";

        }
        
        /// <summary>
        /// Unit
        /// </summary>
        public struct Unit
        {
            /// <summary>
            /// Get Category List
            /// </summary>
            public const string GetAll = Root + "/unit/get-all";

            /// <summary>
            /// Get Unit By it's  Id
            /// </summary>
            public const string Get = Root + "/unit/get/{0}";

            /// <summary>
            /// Create Category
            /// </summary>
            public const string CreateUnit = Root + "/unit/create-unit";

            /// <summary>
            /// Update Category
            /// </summary>
            public const string UpdateUnit = Root + "/unit/update-unit/{0}";

            /// <summary>
            /// Delete Category it's id
            /// </summary>
            public const string Delete = Root + "/unit/delete/{0}";

        }

        /// <summary>
        /// 
        /// </summary>
        public struct Product
        {
            /// <summary>
            /// Get Product List
            /// </summary>
            public const string GetAll = Root + "/product/get-all";

            /// <summary>
            /// Get Product By it's  Id
            /// </summary>
            public const string Get = Root + "/product/get/{0}";

            /// <summary>
            /// Create Product
            /// </summary>
            public const string CreateProduct = Root + "/product/create-product";

            /// <summary>
            /// Update Product
            /// </summary>
            public const string UpdateProduct = Root + "/product/update-product/{0}";

            /// <summary>
            /// Delete Product it's id
            /// </summary>
            public const string Delete = Root + "/product/delete/{0}";
        }

        /// <summary>
        /// Order
        /// </summary>
        public struct Order
        {
            /// <summary>
            /// Get Order List
            /// </summary>
            public const string GetAll = Root + "/order/get-all";

            /// <summary>
            /// Get Order By it's Id
            /// </summary>
            public const string Get = Root + "/order/get/{0}";

            /// <summary>
            /// Creat Order
            /// </summary>
            public const string CreateOrder = Root + "/order/create-order";

            /// <summary>
            /// Update Order
            /// </summary>
            public const string UpdateOrder = Root + "/order/update-order/{0}";

        }

        public struct Cart
        {
            public const string GetItems = Root + "/cart/get-items";

            public const string AddItem = Root + "/cart/add-item";

            public const string UpdateItem = Root + "/cart/update-item/{0}";

            public const string RemoveItem = Root + "/cart/remove-item/{0}";
        }

        public struct Test
        {
            public const string Get = Root + "/Test/" + "Get";
            public const string Post = Root + "/Test/" + "Post";
            
        }
        public struct Values
        {
            public const string GetValues = Root + "/Values/Get";
        }
    }
}
