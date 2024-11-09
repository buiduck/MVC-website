using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using testwebmvc.Context;
using testwebmvc.Models;

namespace testwebmvc.Controllers
{
    public class HomeController : Controller
    {
        DBContext objDBContext = new DBContext();
        public ActionResult Index()
        {
            HomeModel objHomeModel = new HomeModel();

            objHomeModel.ListCategory = objDBContext.Categories.ToList();
            objHomeModel.ListProduct = objDBContext.Products.ToList();
            return View(objHomeModel);
        }
        public ActionResult About()
        {
            return View();
        }
        public ActionResult Contact()
        {
            return View();
        }
        public ActionResult Service()
        {
            return View();
        }
        public ActionResult Sale()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(User _user)
        {
            if (ModelState.IsValid)
            {
                var check = objDBContext.Users.FirstOrDefault(s => s.Email == _user.Email);
                if (check == null)
                {
                    _user.Password = GetMD5(_user.Password);
                    objDBContext.Configuration.ValidateOnSaveEnabled = false;
                    objDBContext.Users.Add(_user);
                    objDBContext.SaveChanges();
                    return RedirectToAction("Login");
                }
                else
                {
                    ViewBag.error = "Email already exists";
                    return View();
                }


            }
            return View();
        }

        //create a string MD5
        public static string GetMD5(string str)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] fromData = Encoding.UTF8.GetBytes(str);
            byte[] targetData = md5.ComputeHash(fromData);
            string byte2String = null;

            for (int i = 0; i < targetData.Length; i++)
            {
                byte2String += targetData[i].ToString("x2");

            }
            return byte2String;
        }
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(User user)
        {
            if (!string.IsNullOrEmpty(user.Email) && !string.IsNullOrEmpty(user.Password))
            {
                var f_password = GetMD5(user.Password);
                var data = objDBContext.Users.Where(s => s.Email.Equals(user.Email) && s.Password.Equals(f_password)).ToList();
                if (data.Count() > 0)
                {
                    //add session
                    Session["FullName"] = data.FirstOrDefault().Firstname + " " + data.FirstOrDefault().Lastname;
                    Session["Email"] = data.FirstOrDefault().Email;
                    Session["idUser"] = data.FirstOrDefault().id;
                    Session["IsAdmin"] = data.FirstOrDefault().IsAdmin ?? false;
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.error = "Login failed";
                    return RedirectToAction("Login");
                }
            }

            ViewBag.ErrorMessage = "Dữ liệu không đúng, vui lòng thử lại!";
            return View();
        }
        //Logout
        public ActionResult Logout()
        {
            Session.Clear();//remove session
            return RedirectToAction("Login");
        }

        // doi mat khau
        public ActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePassword(ChangePasswordModel model)
        {
            if (ModelState.IsValid)
            {
                // Lấy thông tin người dùng từ session
                var userId = (int)Session["idUser"];
                var user = objDBContext.Users.Find(userId);

                // Kiểm tra mật khẩu hiện tại
                var hashedCurrentPassword = GetMD5(model.CurrentPassword);
                if (user.Password != hashedCurrentPassword)
                {
                    ModelState.AddModelError("CurrentPassword", "Mật khẩu hiện tại không đúng.");
                    return View(model);
                }

                // Cập nhật mật khẩu mới
                user.Password = GetMD5(model.NewPassword);
                objDBContext.SaveChanges();

                TempData["SuccessMessage"] = "Đổi mật khẩu thành công.";
                return RedirectToAction("Index"); 
            }

            return View(model);
        }


    }
}