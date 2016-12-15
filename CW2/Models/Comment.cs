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
        [ScaffoldColumn(false)]
        public int Id { get; set; }

        [Display(Name = "Comments")]
        [Required(ErrorMessage = "Please enter content for the comment!")]
        [StringLength(120, ErrorMessage = "Please ensure that the Description does not exced 120")]
        public string CommentDes { get; set; }

        [ScaffoldColumn(false)]
        public int CompareFig { get; set; }

        [ScaffoldColumn(false)]
        public virtual ApplicationUser Student {get;set; }

        [ScaffoldColumn(false)]
        public virtual Anouncement Announc { get; set; }

    }
}