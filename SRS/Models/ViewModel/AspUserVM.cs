using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SRS.Models
{
    
    public class AspUserVm
    {
        public AspUser AspUser { get; set; }

        public IEnumerable<AspRole> AspRole { get; set; }

        public IEnumerable<AspStudentLvl> AspStudentLvl { get; set; }
    }
}