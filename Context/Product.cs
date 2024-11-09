using System.Web.Mvc;

namespace testwebmvc.Context
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Product")]
    public partial class Product
    {
        public int id { get; set; }
        [Display(Name= "Tên sản phẩm")]
        [Required(ErrorMessage = "Name is required.")]
        [StringLength(50)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Avatar is required.")]

        public string Avatar { get; set; }

        [Required(ErrorMessage = "Category ID is required.")]
        public int? CategoryId { get; set; }
        [Required(ErrorMessage = "ShortDes is required.")]
        [AllowHtml]
        [StringLength(50)]
        public string ShortDes { get; set; }
        [Required(ErrorMessage = "FullDescriptiony ID is required.")]
        [AllowHtml]
        [StringLength(50)]
        public string FullDescription { get; set; }

        [Required(ErrorMessage = "Price is required.")]
        public double? Price { get; set; }
        [Required(ErrorMessage = "PriceDiscount is required.")]
        public double? PriceDiscount { get; set; }
        [Required(ErrorMessage = "TypeId is required.")]
        public int? TypeId { get; set; }
        [Required(ErrorMessage = "Slug is required.")]
        [StringLength(50)]
        public string Slug { get; set; }
        [Required(ErrorMessage = "Brand is required.")]
        public int? BrandId { get; set; }
        
        public bool? Deleted { get; set; }

        public bool? ShowOnHomePage { get; set; }

        public int? DisplayOrder { get; set; }

        public DateTime? CreatedOnUtc { get; set; }

        public DateTime? UpdatedOnUTC { get; set; }
    }
}
