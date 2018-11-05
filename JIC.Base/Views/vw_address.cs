using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JIC.Base.Views
{
    public class vw_Address
    {
        [Display(Name = "City", ResourceType = typeof(Resources.Resources))]
        public int? CityID { get; set; }
        [Display(Name = "PoliceStation",ResourceType = typeof(Resources.Resources))]
        public int? PoliceStationID { get; set; }
        [Display(Name = "Address", ResourceType = typeof(Resources.Resources))]
        public string address { get; set; }
    }
}
