using DAL;
using EBBProc.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EBBProc.Areas.Me.Models
{
    public class OrganisationViewModel
    {
        public List<Organisation> Organisations { get; set; }

        public Organisation Row { get; set; }

        public OrganisationForm OrganisationForm { get; set; }

        public SelectList Registrars { get; set; }

        public SelectList States { get; set; }

        public SelectList LGAs { get; set; }

        public List<Document> Documents { get; set; }

        public OrganisationDocumentForm OrganisationDocumentForm { get; set; }

        public SelectList DocumentTypes { get; set; }
    }

    public class OrganisationForm
    {
        public Guid Id { get; set; }

        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Tax Identity Number")]
        public string TIN { get; set; }


        [Required]
        [Display(Name = "Registration Number")]
        public string RCNumber { get; set; }

        [Required]
        [Display(Name = "Registrar")]
        public Guid RegistrarId { get; set; }

        [Display(Name = "Website")]
        public string Website { get; set; }

        [Display(Name = "Registration Year")]
        public int RegistrationYear { get; set; }

        public AddressForm AddressForm { get; set; }
    }

    public class AddressForm
    {
        public Guid Id { get; set; }

        [Required]
        [Display(Name = "Street")]
        public string Street { get; set; }

        [Required]
        [Display(Name = "LGA")]
        public int LGAId { get; set; }

        [Required]
        [Display(Name = "State")]
        public int StateId { get; set; }

        [Required]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Alt. Phone Number")]
        public string AltPhoneNumber { get; set; }

        [Display(Name = "Postal Code")]
        public string PostalCode { get; set; }

        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class OrganisationDocumentForm : DocumentForm
    {
        [Required]
        public Guid OrganisationId { get; set; }
    }
}