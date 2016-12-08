using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CW2.Models
{
    public class Anouncement
    {
        public int Id { get; set; }

        public virtual ApplicationUser Lecture { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public int CountRe { get; set; }

        public virtual IEnumerable<Comment> comment { get; set; }

        //public virtual Comment CommentAcc { get; set; }
    }
}