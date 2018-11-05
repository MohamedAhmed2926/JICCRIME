using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JIC.Base.Views
{
    public class vw_CaseDescription
    {
        public long ID { get; set; }
        public string Description { get; set; }
        public int CaseID { get; set; }
        public string LawItems { get; set; }
    }
}
