using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity.EntityFramework;

namespace EBBProc.Areas.Admin.Models
{
    public class RoleViewModel
    {
        public List<IdentityRole> Roles { get; internal set; }

        public IdentityRole Role { get; set; }

        public RoleForm RoleForm { get; set; }
    }

    public class RoleForm
    {
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}