using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace testwebmvc.Filters
{
    public class AdminAuthorizeAttribute : AuthorizeAttribute
    {

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (httpContext == null)
                throw new ArgumentNullException("httpContext");

            var user = httpContext.Session["IsAdmin"];
            if (user == null)
            {
                return false;
            }

            // Kiểm tra thêm quyền Admin từ session hoặc database nếu cần
            var isAdmin = (bool?)httpContext.Session["IsAdmin"] ?? false;
            return isAdmin;
        }
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            var httpContext = filterContext.HttpContext;
            var isAdmin = (bool?)httpContext.Session["IsAdmin"] ?? false;            
            //var isAdmin = bool.Parse(sessionIsAdminValue);
            if(isAdmin)
            {
                base.HandleUnauthorizedRequest(filterContext);
            }
            else
            {
                var session = httpContext.Session;
                session.Remove("FullName");
                session.Remove("idUser");
                session.Remove("Email");
                session.Remove("IsAdmin");
                filterContext.Result = new RedirectResult("~/Home/Login");

            }
  
        }
    }
}