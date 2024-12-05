using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using testwebmvc.Context;
using testwebmvc.Models;

namespace testwebmvc.Controllers
{
    public class PaymentController : Controller
    {
        DBContext objDBContext = new DBContext();

        // GET: Payment
        public ActionResult Index()
        {
            if (Session["idUser"] == null)
            {
                return RedirectToAction("Login", "Home");
            }

            var lstCart = (List<CartModel>)Session["cart"];
            if (lstCart == null || !lstCart.Any())
            {
                TempData["ErrorMessage"] = "Giỏ hàng đang trống, bạn cần thêm sản phẩm vào giỏ hàng.";
                return RedirectToAction("Index", "Cart");
            }

            // gán dữ liệu cho order
            Order objOrder = new Order();
            objOrder.Name = "Donhang-" + DateTime.Now.ToString("yyyyMMddHHmmss");
            objOrder.UserId = int.Parse(Session["idUser"].ToString());
            objOrder.CreatedOnUtc = DateTime.Now;
            objOrder.Status = 1;

            objDBContext.Orders.Add(objOrder);
            objDBContext.SaveChanges();

            // lấy orderId vừa tạo lưu vào bảng orderdetail
            List<OrderDetail> lstOrderDetail = new List<OrderDetail>();
            foreach (var item in lstCart)
            {
                OrderDetail obj = new OrderDetail();
                obj.Quantity = item.Quantity;
                obj.OrderId = objOrder.id;
                obj.ProductId = item.Product.id;
                lstOrderDetail.Add(obj);
            }

            objDBContext.OrderDetails.AddRange(lstOrderDetail);
            objDBContext.SaveChanges();

            // Clear cart
            lstCart.Clear();
            Session["count"] = null;

            return View();
        }

        public ActionResult VnPay()
        {
            return Redirect("");
        }



    }
}