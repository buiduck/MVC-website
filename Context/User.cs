namespace testwebmvc.Context
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("User")]
    public partial class User
    {
        public int id { get; set; }

        [Required]
        [StringLength(50)]
        public string Firstname { get; set; }
        [Required]
        [StringLength(50)]
        public string Lastname { get; set; }


        [Required(ErrorMessage ="{0} là bắt buộc")]
        [EmailAddress(ErrorMessage = "Invalid Email Address.")]
        [StringLength(50,ErrorMessage ="{0} dài từ {2} tới {1} ký tự",MinimumLength =8)]
        public string Email { get; set; }
        [Required(ErrorMessage = "{0} là bắt buộc")]
        [StringLength(50, ErrorMessage = "{0} dài từ {2} tới {1} ký tự", MinimumLength = 1)]
        public string Password { get; set; }

        public bool? IsAdmin { get; set; }
    }
}