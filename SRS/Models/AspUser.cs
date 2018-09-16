using SRS.Models.ValAttr;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SRS.Models
{
    [Table("tblUser")]
    public class AspUser
    {
        [Key, Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }

        [ForeignKey("AspRoleId")]
        public AspRole AspRole { get; set; }
        [Required]
        [Key, Column(Order = 2)]
        public int AspRoleId { get; set; }

        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [NotMapped]
        public string FullName {
            get
            {
                return FirstName + " " + LastName;
            }
            set
            {
                value = FirstName + " " + LastName;
            }
        }

        [Required]
        [EmailAddress(ErrorMessage = "This Email is Invalid")]
        [MaxLength(120)]
        [CheckEmail_AspUser]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Phone]
        [MaxLength(20)]
        public string Phone { get; set; }


        [ForeignKey("AspStudentLvlId")]
        public AspStudentLvl AspStudentLvl { get; set; }

        [Required]
        [Display(Name = "Student Level")]
        public int AspStudentLvlId { get; set; }

        [CheckStudentId_AspUser]
        [Display(Name = "Student Id")]
        public string StudentID { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid AuthKey { get; set; }
    }
}