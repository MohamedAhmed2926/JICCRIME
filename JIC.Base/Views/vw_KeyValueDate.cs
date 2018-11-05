using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JIC.Base.Views
{
    public class vw_KeyValueDate
    {
        public long ID { get; set; }
        public DateTime Date { get; set; }

        public string StDate { get; set; }
        public string Name { get { return this.Date.ToString(SystemConfigurations.DateTime_ShortDateFormat); } }
    }
}
