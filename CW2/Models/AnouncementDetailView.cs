using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace CW2.Models
{
    public class AnouncementDetailView
    {
        public virtual Anouncement announcement { get; set; }
        
        public virtual Comment comment { get; set; }

       

        public AnouncementDetailView() {
            announcement = new Anouncement();
            comment = new Comment();
        }

    }
}