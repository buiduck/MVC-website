namespace testwebmvc.Context
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Brand")]
    public partial class Brand
    {
        public int id { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(50)]
        public string Avatar { get; set; }

        [StringLength(50)]
        public string Slug { get; set; }

        public bool? ShowOnHomePage { get; set; }

        public int? DisplayOrder { get; set; }

        public DateTime? CreatedOnUtc { get; set; }

        public DateTime? Updated { get; set; }

        public bool? Deleted { get; set; }
    }
}
