using DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EBBProc.Areas.Admin.Models
{
    public class ResourceViewModel
    {
        public List<Resource> Resources { get; set; }

        public Resource Resource { get; set; }

        public ResourceForm ResourceForm { get; set; }
    }

    public class ResourceForm
    {
        public Guid Id { get; set; }

        [Required]
        public Guid? ParentId { get; set; }

        public string Name { get; set; }

        public string Action { get; set; }

        public string Icon { get; set; }

        public string Controller { get; set; }

        public string Area { get; set; }

        public Nullable<int> Order { get; set; }

        public string ModifiedBy { get; set; }

        public System.DateTime ModifiedDate { get; set; }
    }
}