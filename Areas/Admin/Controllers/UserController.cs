using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using testwebmvc.Context;
using testwebmvc.Filters;

namespace testwebmvc.Areas.Admin.Controllers
{
    [AdminAuthorize]
    public class UserController : Controller
    {
        private readonly DBContext db = new DBContext();

        // GET: Admin/User
        public ActionResult Index()
        {
            var users = db.Users.ToList();
            return View(users);
        }

        // GET: Admin/User/Details/5
        public ActionResult Details(int id)
        {
            var user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // GET: Admin/User/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Admin/User/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(User user)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var existingUser = db.Users.Find(user.id);

                    if (existingUser != null)
                    {
                        // Update user properties
                        existingUser.Firstname = user.Firstname;
                        existingUser.Lastname = user.Lastname;
                        existingUser.Email = user.Email;
                        existingUser.IsAdmin = user.IsAdmin;

                        // Save changes
                        db.Entry(existingUser).State = EntityState.Modified;
                        db.SaveChanges();

                        return RedirectToAction("Index");
                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "An error occurred while saving changes: " + ex.Message);
                }
            }

            return View(user);
        }

        // GET: Admin/User/Delete/5
        public ActionResult Delete(int id)
        {
            var user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Admin/User/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var user = db.Users.Find(id);
            if (user != null)
            {
                db.Users.Remove(user);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

    }
}
