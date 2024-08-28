using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using testwebmvc.Context;
using testwebmvc.Models;
namespace testwebmvc.Context
{
    [MetadataType(typeof(UserMaserData))]
    public partial class User
    {
       
    }

    [MetadataType(typeof(UserMasterData))]
    public partial class ProductMasterData
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Avatar { get; set; }
        public string CategoryId { get; set; }
        public decimal Price { get; set; }
        public string PriceDiscount { get; set; }
        public string TypeId { get; set; }
        public string Slug { get; set; }
        public string BrandId { get; set; }
        public DateTime CreatedOnUtc { get; set; }

        [NotMapped]
        public System.Web.HttpPostedFileBase ImgUpload { get; set; }
    }

    internal class UserMasterData
    {
    }
}