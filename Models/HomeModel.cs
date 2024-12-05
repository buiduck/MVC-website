using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using testwebmvc.Context;

namespace testwebmvc.Models
{
    public class HomeModel
    {
        public List<Product> ListProduct { get; set; }
        public List<Category> ListCategory { get; set; }
        public List<User> ListUser { get; set; }
    }

}