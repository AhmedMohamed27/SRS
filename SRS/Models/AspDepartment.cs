using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SRS.Models
{
    [Table("tblDep")]
    public class AspDepartment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DepId { get; set; }

        [Required]
        [MaxLength(20)]
        public string Name { get; set; }
    }
}