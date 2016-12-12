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
        public virtual Anouncement announcement { get; set; }
        [ScaffoldColumn(false)]
        public virtual Comment comment { get; set; }
        [ScaffoldColumn(false)]
        public virtual StudentRead Read { get; set; }

        public AnouncementDetailView() {
            announcement = new Anouncement();
            comment = new Comment();
        }

    }
}