using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JIC.Base.Views
{
   public class vw_InformationPerson
    {
        public string CaseName { get; set; }
        public string OverallNumber { get; set; }
        public string Status { get; set; }
        public string CircuitName { get; set; }
      
        public string UserTypes { get; set; }

        public string Cities { get; set; }
        public string PoliceStations { get; set; }

        public string PhoneNo { get; set; }
        public string Nationalities { get; set; }
        public string NatNo { get; set; }
        public string Name { get; set; }
        public string Job { get; set; }
        public string PassportNo { get; set; }
        public string address { get; set; }
        public string BirthDate { get; set; }
        public string Age { get; set; }

        public List<vw_cases> CasesList = new List<vw_cases>();
    }

    public class vw_cases
    {
        public string CaseName { get; set; }
        public string OverallNumber { get; set; }
        public string Status { get; set; }
    }

    }
