using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SRS.Models
{
    [Table("tblGPA")]
    public class AspGPA
    {
        [Key,Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int GpaId { get; set; }

        public int Takenhoures { get; set; }

        public int Pendinghoures { get; set; }

        public decimal GpaVal { get; set; }
        
        [ForeignKey("UserId,RoleId")]
        public AspUser User { get; set; }
        [Required]
        [Key, Column(Order = 2)]
        public int UserId { get; set; }

        [Required]
        [Key, Column(Order = 3)]
        public int RoleId { get; set; }
    }
}