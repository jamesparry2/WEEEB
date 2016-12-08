using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;

namespace CW2.Models
{
    public class Comment
    {
        public int Id { get; set; }

        [Display(Name = "Comment Description")]
        public string CommentDes { get; set; }

        public int CompareFig { get; set; }

        public virtual ApplicationUser Student {get;set; }

        public virtual Anouncement announc { get; set; }

    }
}