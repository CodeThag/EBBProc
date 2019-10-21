using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EBBProc.Common
{
    public class DocumentForm
    {
        [Required]
        [Display(Name = "Document Type")]
        public Guid DocumentType { get; set; }

        [Required]
        [Display(Name = "Select File")]
        public HttpPostedFileBase File { get; set; }
    }
}