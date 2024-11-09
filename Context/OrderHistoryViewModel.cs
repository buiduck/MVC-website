using System;
using System.Collections.Generic;

namespace testwebmvc.ViewModels
{
    public class OrderHistoryViewModel
    {
        public int OrderId { get; set; }
        public string OrderName { get; set; }
        public int UserId { get; set; }
        public DateTime? CreatedOnUtc { get; set; }
        public int Status { get; set; }
        public List<OrderDetailViewModel> OrderDetails { get; set; }

        public OrderHistoryViewModel() // Constructor
        {
            OrderDetails = new List<OrderDetailViewModel>();
        }
    }

    public class OrderDetailViewModel
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
    }
}
