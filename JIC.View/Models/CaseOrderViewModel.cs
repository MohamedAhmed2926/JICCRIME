using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JIC.Crime.View.Models
{
    public class CaseOrderViewModel
    {
        public int Order { get; set; }
        public int CaseID { get; set; }
        public int SecritaryID { get; set; }
    }
}