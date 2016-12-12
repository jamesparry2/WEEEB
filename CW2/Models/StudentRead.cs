using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CW2.Models
{
    public class StudentRead
    {
        [ScaffoldColumn(false)]
        public int Id { get; set; }
        [ScaffoldColumn(false)]
        public virtual Anouncement AnnounceId { get; set; }
        [ScaffoldColumn(false)]
        public virtual ApplicationUser UserId { get; set; }
    }
}