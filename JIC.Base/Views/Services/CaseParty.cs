using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace JIC.Base.Views.Services
{
    [DataContract]
    public class CaseParty
    {
        [DataMember]
        public int ID { get; set; }
        [DataMember]
        public int Order { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string Nationality { get; set; }
        public int NationalityID { get; set; }
        [DataMember]
        public string Passport { get; set; }
        [DataMember]
        public string NationalID { get; set; }
        [DataMember]
        public bool IsCivilRightProsecutor { get; set; }
        [DataMember]
        public int PartyType { get; set; }
        [DataMember]
        public bool IsLegalPerson { get; set; }
        [DataMember]
        public LegalPerson LegalPerson { get; set; }
        [DataMember]
        public int? DefendantPoliceStationStatusID { get; set; }
        [DataMember]
        public List<int> DefendantCharges { get; set; }

        [DataMember]
        public DateTime? BirthDate { get; set; }
        [DataMember]
        public string Job { get; set; }
        [DataMember]
        public string PhoneNo { get; set; }
        [DataMember]
        public string Address { get; set; }
        [DataMember]
        public int? Address_City_ID { get; set; }
        [DataMember]
        public int? Address_Police_Station_ID { get; set; }

        public vw_CaseDefectData MapCaseDefectData(int CaseID,long PersonID)
        {
            return new vw_CaseDefectData
            {
                CaseID = CaseID,
                Crimes = DefendantCharges,
                DefectType = (PartyTypes)PartyType,
                DefendantStatus = DefendantPoliceStationStatusID,
                IsCivilRightProsecutor = IsCivilRightProsecutor,
                Order = Order,
                PersonID = PersonID

            };
        }

        public vw_PersonData MapPersonData()
        {
            return new vw_PersonData
            {
                BirthDate = BirthDate,
                Job = Job,
                Name = Name,
                NationalityID = NationalityID,
                NatNo = NationalID,
                PassportNo = Passport,
                PhoneNo = PhoneNo,
                CleanFullName = Base.Utilities.RemoveSpaces(Base.Utilities.RemoveSpecialCharacters(Name)),
                address = String.IsNullOrEmpty(Address) ? null : new vw_Address { address = Address,CityID = Address_City_ID,PoliceStationID = Address_Police_Station_ID }
            };
        }
    }
}
