using System.ComponentModel.DataAnnotations;
using System;
using testwebmvc.Context;
namespace testwebmvc.Models
{
    public partial class UserMaserData
    {
        public int id { get; set; }
        [Required]
        [StringLength(50)]
        public string Firstname { get; set; }
        [Required]
        [StringLength(50)]
        public string Lastname { get; set; }
        [Required]
        [EmailAddress(ErrorMessage = "Invalid Email Address.")]
        [StringLength(50)]
        public string Email { get; set; }
        [StringLength(50)]
        public string Password { get; set; }
        public bool? IsAdmin { get; set; }

    }

}