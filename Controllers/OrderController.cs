using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using testwebmvc.Context;
using testwebmvc.ViewModels; // Đừng quên thêm namespace cho ViewModels

namespace testwebmvc.Controllers
{
    public class OrderController : Controller
    {
        private DBContext db = new DBContext();

        // Action để hiển thị lịch sử đơn hàng của người dùng
        public ActionResult OrderHistory()
        {
            // Kiểm tra xem người dùng đã đăng nhập chưa
            if (Session["idUser"] == null)
            {
                return RedirectToAction("Login", "Home"); // Redirect đến trang đăng nhập nếu chưa đăng nhập
            }

            int currentUserId = (int)Session["idUser"]; // Lấy ID người dùng từ session

            // Lấy danh sách đơn hàng của người dùng từ cơ sở dữ liệu
            var orders = db.Orders.Where(o => o.UserId == currentUserId).ToList();

            // Tạo danh sách ViewModel để chuyển tới view
            var orderHistoryList = new List<OrderHistoryViewModel>();

            foreach (var order in orders)
            {
                // Lấy danh sách chi tiết đơn hàng
                var orderDetails = db.OrderDetails
                    .Where(od => od.OrderId == order.id)
                    .Select(od => new OrderDetailViewModel
                    {
                        ProductId = od.ProductId,
                        ProductName = db.Products.FirstOrDefault(p => p.id == od.ProductId).Name, // Lấy tên sản phẩm
                        Quantity = od.Quantity
                    }).ToList();

                // Ánh xạ dữ liệu từ bảng Order và OrderDetail vào ViewModel
                var orderHistory = new OrderHistoryViewModel
                {
                    OrderId = order.id,
                    OrderName = order.Name,
                    UserId = order.UserId ?? 0,
                    CreatedOnUtc = order.CreatedOnUtc,
                    Status = order.Status ?? 0,
                    OrderDetails = orderDetails
                };

                // Thêm vào danh sách để truyền vào view
                orderHistoryList.Add(orderHistory);
            }

            return View(orderHistoryList); // Truyền danh sách OrderHistoryViewModel vào view
        }
    }
}
