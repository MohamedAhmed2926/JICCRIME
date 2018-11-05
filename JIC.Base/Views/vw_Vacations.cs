using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JIC.Base.Views
{
  public  class vw_Vacations
    {
        public long ID { get; set; }
        public string Name { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
