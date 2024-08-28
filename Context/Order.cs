namespace testwebmvc.Context
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Order")]
    public partial class Order
    {
        public int id { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        public int? UserId { get; set; }

        public int? Status { get; set; }

        public DateTime? CreatedOnUtc { get; set; }
    }
}
