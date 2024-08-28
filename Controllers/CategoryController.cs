using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using testwebmvc.Context;

namespace testwebmvc.Controllers
{
    public class CategoryController : Controller
    {
        // GET: Category

        DBContext objDBContext = new DBContext();
        public ActionResult Index()
        {
            var lstCategory = objDBContext.Categories.ToList();
            return View(lstCategory);
        }

        public ActionResult ProductCategory(int id)
        {
            var lstProduct = objDBContext.Products.Where(n => n.CategoryId == id).ToList();
            return View(lstProduct);
        }
    }

}