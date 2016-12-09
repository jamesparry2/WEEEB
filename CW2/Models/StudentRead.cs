using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CW2.Models
{
    public class StudentRead
    {
        public int Id { get; set; }
        public virtual Anouncement AnnounceId { get; set; }
        public virtual ApplicationUser UserId { get; set; }
    }
}