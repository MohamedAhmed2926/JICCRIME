using JIC.Base.Resources;
using JIC.Base.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace JIC.Crime.View.Models
{
    public class CaseConfigurationViewModel
    {
        public CaseConfigurationViewModel()
        {
            Cases = new List<int>();
        }
        public List<int> Cases { get; set; }
        [Required(ErrorMessageResourceType = typeof(Messages),
           ErrorMessageResourceName = "RequiredErrorMessage")]
        [Display(Name = "Session", ResourceType = typeof(JIC.Base.Resources.Resources))]
        public long SessionID { get; set; }
        [Required(ErrorMessageResourceType = typeof(Messages),
           ErrorMessageResourceName = "RequiredErrorMessage")]
        [Display(Name = "Circuit", ResourceType = typeof(JIC.Base.Resources.Resources))]
        public int CircuitID { get; set; }


        public DateTime SessionDate { get; set; }
    }

    public class CaseConfigurationData
    {
        public CaseConfigurationData()
        {
            Circuits = new List<vw_KeyValue>();
            Sessions = new List<vw_KeyValueLongID>();
        }
        public CaseConfigurationViewModel caseConfiguration { get; set; }
        public List<vw_KeyValue> Circuits { get; set; }
        public List<vw_KeyValueLongID> Sessions { get; set; }
    }

}