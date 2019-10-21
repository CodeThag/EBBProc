using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EBBProc.Areas.Admin.Models
{
    public class MOrganisationViewModel
    {
        public List<Organisation> Organisations { get; set; }
        public Organisation Row { get; set; }

        public StatusForm StatusForm { get; set; }
        public SelectList OrganisationStatus { get; set; }
    }

    public class StatusForm
    {
        public Guid Id { get; set; }

        public Guid Status { get; set; }
    }
}