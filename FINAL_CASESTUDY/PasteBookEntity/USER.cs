//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PasteBookEntity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class USER
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public USER()
        {
            this.COMMENTs = new HashSet<COMMENT>();
            this.FRIENDs = new HashSet<FRIEND>();
            this.FRIENDs1 = new HashSet<FRIEND>();
            this.LIKEs = new HashSet<LIKE>();
            this.NOTIFICATIONs = new HashSet<NOTIFICATION>();
            this.NOTIFICATIONs1 = new HashSet<NOTIFICATION>();
            this.POSTs = new HashSet<POST>();
            this.POSTs1 = new HashSet<POST>();
        }
    
        public int ID { get; set; }

        [Required(ErrorMessage ="Username field is required.")]
        [RegularExpression("^(([a-zA-Z0-9]+([_.]?))+[a-zA-Z0-9]+)$", ErrorMessage = "Username can only contain letters, numbers ,underscores(_), and periods(.), but cannot have two or more consecutive underscores and periods.")]
        [StringLength(50, ErrorMessage = "Max Length for this field is 50 characters only.")]
        public string USER_NAME { get; set; }

        [Required(ErrorMessage = "Email field is required.")]        
        [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
        [StringLength(50, ErrorMessage = "Max Length for this field is 50 characters only.")]
        public string EMAIL_ADDRESS { get; set; }

        [Required(ErrorMessage = "Password field is required.")]
        [StringLength(50, ErrorMessage = "Max Length for this field is 50 characters only.")]
        public string PASSWORD { get; set; }

        public string SALT { get; set; }

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

        public byte[] PROFILE_PIC { get; set; }

        public System.DateTime DATE_CREATED { get; set; }

        public string ABOUT_ME { get; set; }
        
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<COMMENT> COMMENTs { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FRIEND> FRIENDs { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FRIEND> FRIENDs1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LIKE> LIKEs { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<NOTIFICATION> NOTIFICATIONs { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<NOTIFICATION> NOTIFICATIONs1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<POST> POSTs { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<POST> POSTs1 { get; set; }
        public virtual REF_COUNTRY REF_COUNTRY { get; set; }
    }
}
