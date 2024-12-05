using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using testwebmvc.Context;

namespace testwebmvc.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        DBContext objDBContext = new DBContext();
        public ActionResult Detail(int id )
        {
            var objProduct = objDBContext.Products.FirstOrDefault(n => n.id == id);
            if(objProduct != null)
            {
                return View(objProduct);

            }
            return RedirectToAction("Index", "Home");
        }
    }
}