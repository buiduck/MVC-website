using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using testwebmvc.Context;

namespace testwebmvc.Models
{
    public class CartModel
    {
        public decimal TotalPrice { get; set; }
        public  Product Product { get; set; }
        public int Quantity { get; set; }
        public int ProductId { get; set; }

    }
    public class CartModelLite
    {
        public int Quantity { get; set; }
        public int ProductId { get; set; }
    }
}