using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using testwebmvc.Context;
using testwebmvc.Models;


namespace testwebmvc.Controllers
{
    public class CartController : Controller
    {
        DBContext objDBContext = new DBContext();
        // GET: Cart
        public ActionResult Index()
        {
            List<CartModel> cartItems = (List<CartModel>)Session["cart"];

            if (cartItems != null && cartItems.Any())
            {
                decimal totalCartPrice = CalculateTotalCartPrice(cartItems);
                ViewBag.TotalCartPrice = totalCartPrice;
                return View(cartItems);
            }
            else
            {
                // Nếu giỏ hàng trống, trả về view mà không có dữ liệu sản phẩm
                return View(new List<CartModel>());
            }

        }
        [HttpPost]
        private decimal CalculateTotalCartPrice(List<CartModel> cartItems)
        {
            decimal totalCartPrice = 0;

            foreach (var item in cartItems)
            {
                // Tính toán tổng giá cho từng mục (số lượng * giá)
                item.TotalPrice = (decimal)(item.Quantity * item.Product.Price);
                totalCartPrice += item.TotalPrice;
            }

            return totalCartPrice;
        }
        [HttpPost]
        public ActionResult AddToCart(int id, int quantity)
        {
            if (Session["cart"] == null)
            {
                List<CartModel> cart = new List<CartModel>();

                var product = objDBContext.Products.Find(id);
                //nếu không tồn tại thì thêm sản phẩm vào giỏ hàng
                cart.Add(new CartModel
                {
                    Product = product,
                    Quantity = quantity,
                    ProductId = product.id
                });
                foreach (var item in cart)
                {
                    item.TotalPrice = (decimal)(item.Quantity * item.Product.Price * 1.0);
                }
                Session["cart"] = cart;
                Session["count"] = 1;
            }
            else
            {
                List<CartModel> cart = (List<CartModel>)Session["cart"];
                //kiểm tra sản phẩm có tồn tại trong giỏ hàng chưa???
                int index = ProductPosittionInSession(id);
                if (index != -1)
                {
                    var currentItem = cart[index];
                    //nếu sp tồn tại trong giỏ hàng thì cộng thêm số lượng
                    currentItem.Quantity += quantity;
                    currentItem.TotalPrice += (decimal)currentItem.Product.Price * quantity;
                }
                else
                {
                    var product = objDBContext.Products.Find(id);
                    //nếu không tồn tại thì thêm sản phẩm vào giỏ hàng
                    cart.Add(new CartModel
                    {
                        Product = product,
                        Quantity = quantity,
                        ProductId = product.id,
                        TotalPrice = (decimal)product.Price * quantity
                    });


                    //Tính lại số sản phẩm trong giỏ hàng
                    Session["count"] = Convert.ToInt32(Session["count"]) + 1;
                }
                Session["cart"] = cart;
            }
            return Json(new { Message = "Thành công", JsonRequestBehavior.AllowGet });
        }
        private int ProductPosittionInSession(int id)
        {
            List<CartModel> cart = (List<CartModel>)Session["cart"];
            for (int i = 0; i < cart.Count; i++)
                if (cart[i].Product.id.Equals(id))
                    return i;
            return -1;
        }
        //xóa sản phẩm khỏi giỏ hàng theo id
        [HttpPost]
        public ActionResult Remove(int id)
        {
            List<CartModel> cart = (List<CartModel>)Session["cart"];
            if (cart != null && cart.Any())
            {
                int index = ProductPosittionInSession(id);
                if (index != -1)
                {
                    cart.RemoveAt(index);
                    // Cập nhật giỏ hàng trong Session
                    Session["cart"] = cart;
                    // Cập nhật tổng giá trong Session
                    decimal totalCartPrice = CalculateTotalCartPrice(cart);
                    Session["totalCartPrice"] = totalCartPrice;
                    // Cập nhật số sản phẩm trong giỏ hàng
                    Session["count"] = Convert.ToInt32(Session["count"]) - 1;
                    return Json(new { Success = true, TotalCartPrice = totalCartPrice }, JsonRequestBehavior.AllowGet);
                }
            }
            return Json(new { Success = false }, JsonRequestBehavior.AllowGet);
        }


        // Cập nhật giỏ hàng sau khi thay đổi số lượng
        // GetProductPrice được thêm vào

        private decimal GetProductPrice(int productId)
        {
            // Lấy giá sản phẩm từ cơ sở dữ liệu hoặc nguồn dữ liệu khác
            Product product = objDBContext.Products.Find(productId);
            return (decimal)product.Price;
        }

        // Cập nhật giỏ hàng sau khi thay đổi số lượng 
        [HttpGet]
        public ActionResult UpdateCartItem(int productId, int quantity)
        {

            List<CartModel> cart = (List<CartModel>)Session["cart"];
            if (cart != null && cart.Any())
            {
                int index = ProductPosittionInSession(productId);

                if (index != -1)
                {
                    
                    // Cập nhật số lượng
                    cart[index].Quantity = quantity;

                    // Lấy giá sản phẩm đã cập nhật
                    decimal updatedPrice = GetProductPrice(cart[index].ProductId);

                    // Cập nhật giá và tổng giá dựa trên số lượng và giá đã cập nhật
                    // Kiểm tra kiểu dữ liệu của Product.Price (khuyến khích là decimal)
                    if (cart[index].Product.Price.GetType() == typeof(decimal))
                    {
                        cart[index].Product.Price = (double?)Convert.ToDecimal(updatedPrice);
                    }
                    else
                    {
                        cart[index].Product.Price = (double?)Convert.ToDecimal(updatedPrice);  // Ép sang double? nếu cần
                    }

                    cart[index].TotalPrice = (decimal)(cart[index].Quantity * cart[index].Product.Price);

                    // Cập nhật giỏ hàng trong Session
                    Session["cart"] = cart;

                    // Tính toán lại tổng giá giỏ hàng
                    decimal totalCartPrice = CalculateTotalCartPrice(cart);

                    return Json(new
                    {
                        Success = true,
                        TotalCartPrice = totalCartPrice,
                        CartItemPrice = updatedPrice,
                        CartItemTotalPrice = cart[index].TotalPrice
                    }, JsonRequestBehavior.AllowGet);
                }
            }
            return Json(new { Success = false }, JsonRequestBehavior.AllowGet);
        }
    }
}