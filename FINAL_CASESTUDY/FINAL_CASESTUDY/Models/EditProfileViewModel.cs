using PasteBookEntity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FINAL_CASESTUDY.Models
{
    public class EditProfileViewModel
    {
        [Required(ErrorMessage = "Username field is required.")]
        [RegularExpression("^(([a-zA-Z0-9]+([_.]?))+[a-zA-Z0-9]+)$", ErrorMessage = "Username can only contain letters, numbers ,underscores(_), and periods(.), but cannot have two or more consecutive underscores and periods.")]
        [StringLength(50, ErrorMessage = "Max Length for this field is 50 characters only.")]
        public string USER_NAME { get; set; }

        [Required(ErrorMessage = "First name field is required.")]
        [RegularExpression(@"^([\s]*([a-zA-Z0-9]+[ '.-]?[\s]*)+[a-zA-Z0-9]+[ '.-]?[\s]*)$", ErrorMessage = "First name can only contain letters, numbers, apostrophes('), hyphens(-) and periods(.), but cannot have two or more consecutive apostrophes, hyphens and periods.")]
        [StringLength(50, ErrorMessage = "Max Length for this field is 50 characters only.")]
        public string FIRST_NAME { get; set; }

        [Required(ErrorMessage = "Last name field is required.")]
        [RegularExpression(@"^([\s]*([a-zA-Z0-9]+[ '.-]?[\s]*)+[a-zA-Z0-9]+[ '.-]?[\s]*)$", ErrorMessage = "Last name can only contain letters, numbers, apostrophes('), hyphens(-) and periods(.), but cannot have two or more consecutive apostrophes, hyphens and periods.")]
        [StringLength(50, ErrorMessage = "Max Length for this field is 50 characters only.")]
        public string LAST_NAME { get; set; }

        [Required(ErrorMessage = "Birthday field is required.")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public System.DateTime BIRTHDAY { get; set; }

        public Nullable<int> COUNTRY_ID { get; set; }

        [DataType(DataType.PhoneNumber)]
        [Phone(ErrorMessage = "Please enter a valid mobile number.")]
        public string MOBILE_NO { get; set; }

        public string GENDER { get; set; }
    }
}