using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EBBProc.Areas.Me.Models
{
    public class ProfileViewModel
    {
        public ProfileForm ProfileForm { get; set; }
    }


    public class ProfileForm
    {
        [Required]
        [Display(Name = "First Name")]
        public string Firstname { get; set; }

        [Required]
        [Display(Name = "Surname")]
        public string Surname { get; set; }

        [Required]
        [Display(Name = "Profession")]
        public string Profession { get; set; }
    }
}