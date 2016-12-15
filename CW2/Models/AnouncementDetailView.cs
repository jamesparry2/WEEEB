using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNet.Identity.EntityFramework;

namespace CW2.Models
{
    public class AnouncementDetailView
    {
        [ScaffoldColumn(false)]
        public virtual Anouncement Announcement { get; set; }
        [ScaffoldColumn(false)]
        public virtual Comment Comment { get; set; }
        [ScaffoldColumn(false)]
        public virtual StudentRead Read { get; set; }

        public AnouncementDetailView() {
            Announcement = new Anouncement();
            Comment = new Comment();
        }

    }
}