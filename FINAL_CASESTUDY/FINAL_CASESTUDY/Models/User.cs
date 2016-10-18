using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FINAL_CASESTUDY.Models
{
    public class User
    {
        public int UserID { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string ConfirmPassword { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        [DisplayFormat(DataFormatString = "{0:MMM d, yyyy}")]
        public DateTime Birthday { get; set; }

        public int? CountryID { get; set; }
        public string MobileNumber { get; set; }
        public string Gender { get; set; }
        public byte[] ProfilePicture { get; set; }
        public string AboutMe { get; set; }
    }
}