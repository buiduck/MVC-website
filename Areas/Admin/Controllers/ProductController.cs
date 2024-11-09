using PagedList;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using testwebmvc.Context;
using testwebmvc.Filters;
using static testwebmvc.Common;

namespace testwebmvc.Areas.Admin.Controllers
{
    [AdminAuthorize]
    public class ProductController : Controller
    {
        DBContext objDBContext = new DBContext();
        // GET: Admin/Product
        public ActionResult Index(string currentFilter, string SearchString, int? page)
        {
            var lstProduct = new List<Product>();
            if (SearchString != null)
            {
                page = 1;
            }
            else
            {
                SearchString = currentFilter;
            }
            if (!string.IsNullOrEmpty(SearchString))
            {
                // lay ds san pham theo key timkiem
                lstProduct = objDBContext.Products.Where(n => n.Name.Contains(SearchString)).ToList();

            }
            else
            {
                //lay all sp trong product
                lstProduct = objDBContext.Products.ToList();
            }
            ViewBag.CurrentFilter = SearchString;
            // Kiểm tra xem có sản phẩm nào được tìm thấy không
            if (!lstProduct.Any())
            {
                ViewBag.Message = "Không có sản phẩm phù hợp với từ khóa";
            }
            //so luong item trong 1 trang =4
            int pageSize = 4;
            int pageNumber = (page ?? 1);
            //sap xep theo id san pham , sp moi len dau
            lstProduct = lstProduct.OrderByDescending(n => n.id).ToList();
            return View(lstProduct.ToPagedList(pageNumber, pageSize));
        }
        public ActionResult Details(int id)
        {
            var objProduct = objDBContext.Products.Where(n => n.id == id).FirstOrDefault();
            return View(objProduct);
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var objProduct = objDBContext.Products.Where(n => n.id == id).FirstOrDefault();
            return View(objProduct);
        }
        [HttpPost]
        public ActionResult Delete(Product product)
        {
            var objProduct = objDBContext.Products.Where(n => n.id == product.id).FirstOrDefault();
            objDBContext.Products.Remove(objProduct);
            objDBContext.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            this.LoadData();
            var objProduct = objDBContext.Products.Where(n => n.id == id).FirstOrDefault();
            return View(objProduct);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Product product)
        {
            this.LoadData();
            var favatar = Request.Files["favatar"];
            var existingProduct = objDBContext.Products.Find(id);

            if (existingProduct != null)
            {
                if (favatar != null && favatar.ContentLength > 0)
                {
                    string fileName = favatar.FileName;
                    string saveImageDirectory = Server.MapPath("~/Content/images/" + fileName);
                    favatar.SaveAs(saveImageDirectory);
                    product.Avatar = "/Content/images/" + fileName;
                }
                else
                {
                    product.Avatar = existingProduct.Avatar;
                }

                existingProduct.Name = product.Name;
                existingProduct.CategoryId = product.CategoryId;
                existingProduct.ShortDes = product.ShortDes;
                existingProduct.FullDescription = product.FullDescription;
                existingProduct.Price = product.Price;
                existingProduct.PriceDiscount = product.PriceDiscount;
                existingProduct.TypeId = product.TypeId;
                existingProduct.Slug = product.Slug;
                existingProduct.BrandId = product.BrandId;
                existingProduct.Deleted = product.Deleted;
                existingProduct.ShowOnHomePage = product.ShowOnHomePage;
                existingProduct.DisplayOrder = product.DisplayOrder;
                existingProduct.CreatedOnUtc = product.CreatedOnUtc;
                existingProduct.UpdatedOnUTC = DateTime.UtcNow;
                existingProduct.Avatar = product.Avatar;

                try
                {
                    objDBContext.Entry(existingProduct).State = EntityState.Modified;
                    objDBContext.SaveChanges();
                }
                catch (DbEntityValidationException ex)
                {
                    foreach (var entityValidationErrors in ex.EntityValidationErrors)
                    {
                        foreach (var validationError in entityValidationErrors.ValidationErrors)
                        {
                            System.Diagnostics.Debug.WriteLine("Property: " + validationError.PropertyName + " Error: " + validationError.ErrorMessage);
                        }
                    }
                    throw;
                }

                return RedirectToAction("Index");
            }

            return View(product);
        }

        [HttpGet]
        public ActionResult AddSP()
        {

            this.LoadData();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddSP(Product obj)
        {
            this.LoadData();
            if (ModelState.IsValid)
            {
                try
                {
                    var favatar = Request.Files["favatar"];
                    if (favatar != null && favatar.ContentLength > 0)
                    {
                        string fileName = favatar.FileName;
                        string saveImageDirectory = Server.MapPath("~/Content/images/" + fileName);
                        favatar.SaveAs(saveImageDirectory);
                        obj.Avatar = "/Content/images/" + fileName;
                    }
                    objDBContext.Products.Add(obj);
                    objDBContext.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch
                {

                }

            }
            return View(obj);
        }
        void LoadData()
        {
            Common objCommon = new Common();
            var lstCat = objDBContext.Categories.ToList();
            //Convert sang select list dang value,text
            ListtoDataTableConverter converter = new ListtoDataTableConverter();
            DataTable dtCategory = converter.ToDataTable(lstCat);
            ViewBag.ListCategory = objCommon.ToSelectList(dtCategory, "id", "Name");

            // lay du lieu thuong hieu tu db
            var lstBrand = objDBContext.Brands.ToList();
            DataTable dtBrand = converter.ToDataTable(lstBrand);
            //Convert sang select list value ,text
            ViewBag.ListBrand = objCommon.ToSelectList(dtBrand, "id", "Name");

            // Loại sản phẩm
            List<ProductType> lstProductType = new List<ProductType>();
            ProductType objProductType = new ProductType();
            objProductType.Id = 01;
            objProductType.Name = "Đề xuất";
            lstProductType.Add(objProductType);

            objProductType = new ProductType();
            objProductType.Id = 02;
            objProductType.Name = "Giảm giá sốc";
            lstProductType.Add(objProductType);

            DataTable dtProductType = converter.ToDataTable(lstProductType);
            //Convert sang select list value ,text
            ViewBag.ProductType = objCommon.ToSelectList(dtProductType, "id", "Name");

        }
    }
}