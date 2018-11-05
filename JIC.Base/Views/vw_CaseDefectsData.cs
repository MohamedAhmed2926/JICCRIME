using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JIC.Base.Views
{
   public class vw_CaseDefectsData
    {
        public long ID { get; set; }
        public int CaseID { get; set; }
        public long PersonID { get; set; }
        public string Name { get; set; }
        public int?  NationalityType { get; set; }
        public string NationalID { get; set; }
        public DateTime?  Birthdate { get; set; }
        public long Age { get; set; }
        public  PartyTypes DefectType { get; set; }
        public int Status { get; set; }
        public List<vw_KeyValue> Crimes { get; set; }
        public bool IsCivilRightProsecutor { get; set; }
        public string Address { get; set; }
        public string JobName { get; set; }
        public string PassportNumber { get; set; }
       public PresenceStatus Presence { get; set; }
        public int Order { get; set; }
        public string PhoneNo { get; set; }
        public int? PoliceStationID { get; set; }
        public int? CityID { get; set; }
        public string Nationality { get; set; }
    }
}
