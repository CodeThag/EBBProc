using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EBBProc.Common
{
    public class PersonForm
    {
        public Guid Id { get; set; }

        [Required]
        [Display(Name = "Surname")]
        public string Surname { get; set; }

        [Required]
        [Display(Name = "First name")]
        public string Firstname { get; set; }

        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }
    }

    public class DegreeForm
    {
        public Guid UserId { get; set; }

        [Required]
        [Display(Name = "Degree/Professional")]
        public string Degree { get; set; }

        [Display(Name = "Year Obtained")]
        public string Professional { get; set; }

        [Display(Name = "Awarding Institution")]
        public string AwardingInstitution { get; set; }
    }
}