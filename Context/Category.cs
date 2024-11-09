namespace testwebmvc.Context
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Category")]
    public partial class Category
    {
        public int id { get; set; }
        [Required(ErrorMessage = "Name is required.")]
        [StringLength(50)]
        public string Name { get; set; }
        [StringLength(100)]
        public string Avatar { get; set; }
        [Required(ErrorMessage = "Slug is required.")]
        [StringLength(50)]
        public string Slug { get; set; }

        public bool? ShowOnHomePage { get; set; }

        public int? DisplayOrder { get; set; }

        public bool? Deleted { get; set; }

        public DateTime? CreatedOnUtc { get; set; }

        public DateTime? UpdatedOnUtc { get; set; }
    }
}
