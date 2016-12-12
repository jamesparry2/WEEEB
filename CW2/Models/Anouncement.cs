using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CW2.Models
{
    public class Anouncement
    {
        [ScaffoldColumn(false)]
        public int Id { get; set; }

        [ScaffoldColumn(false)]
        public virtual ApplicationUser Lecture { get; set; }

        [Required(ErrorMessage = "Please enter a 'Title' for the Anouncement!")]
        [StringLength(30, MinimumLength = 5, ErrorMessage = "Ensure the Title is less than 30 but more that 5")]
        [Display(Name = "Anouncement: ")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Please enter a description for the Anouncement")]
        [StringLength(60, ErrorMessage = "Please ensure that the Description does not exced 60")]
        [Display(Name = "Content: ")]
        public string Description { get; set; }

        [ScaffoldColumn(false)]
        [Display(Name = "Number of uniqe visits: ")]
        public int CountRe { get; set; }

        [ScaffoldColumn(false)]
        public virtual IEnumerable<Comment> comment { get; set; }

    }
}