using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace JIC.Base.Views.ProsecutionService
{
    [DataContract]
    public class parParties
    {
        [DataMember]
        public string fullName { get; set; }
        [DataMember]
        public string address { get; set; }
        [DataMember]
        public string age { get; set; }
        [DataMember]
        public int? businessCaseDetailsId { get; set; }
        [DataMember]
        public int? businessCaseId { get; set; }
        [DataMember]
        public int? city { get; set; }
        [DataMember]
        public string createdBy { get; set; }
        [DataMember]
        public int? createdByBusinessCaseId { get; set; }
        [DataMember]
        public DateTime? createdDate { get; set; }
        [DataMember]
        public DateTime? dateOfBirth { get; set; }
        [DataMember]
        public string duty { get; set; }
        [DataMember]
        public string educationalEntityAddress { get; set; }
        [DataMember]
        public int? educationalEntityId { get; set; }
        [DataMember]
        public string educationalEntityName { get; set; }
        [DataMember]
        public int? educationalLevelId { get; set; }
        [DataMember]
        public string entityName { get; set; }
        [DataMember]
        public string entityRepresentative { get; set; }
        [DataMember]
        public string extraInformation { get; set; }
        [DataMember]
        public string flex1 { get; set; }
        [DataMember]
        public string flex2 { get; set; }
        [DataMember]
        public string flex3 { get; set; }
        [DataMember]
        public string flex4 { get; set; }
        [DataMember]
        public string fname { get; set; }
        [DataMember]
        public string fourthname { get; set; }
        [DataMember]
        public string gender { get; set; }
        [DataMember]
        public int? govermentDegreeId { get; set; }
        [DataMember]
        public string govermentEmployer { get; set; }
        [DataMember]
        public string govermentEmployerAddress { get; set; }
        [DataMember]
        public DateTime? govermentJoiningDate { get; set; }
        [DataMember]
        public string guardianName { get; set; }
        [DataMember]
        public int? guardianNationalityId { get; set; }
        [DataMember]
        public int id { get; set; }
        [DataMember]
        public string idNumber { get; set; }
        [DataMember]
        public int? idType { get; set; }
        [DataMember]
        public DateTime? issueDate { get; set; }
        [DataMember]
        public string jobTitle { get; set; }
        [DataMember]
        public string militaryCurrentUnit { get; set; }
        [DataMember]
        public int? militaryRankId { get; set; }
        [DataMember]
        public string militaryRecruitmentId { get; set; }
        [DataMember]
        public int? militaryServiceCorpsId { get; set; }
        [DataMember]
        public int? moiPrisonId { get; set; }
        [DataMember]
        public string moiPrisonNumber { get; set; }
        [DataMember]
        public string nationality { get; set; }
        [DataMember]
        public int? nearlyGuardianId { get; set; }
        [DataMember]
        public string nickname { get; set; }
        [DataMember]
        public int? parId { get; set; }
        [DataMember]
        public int? partyStatus { get; set; }
        [DataMember]
        public int partyType { get; set; }
        [DataMember]
        public string partyTypeName { get; set; }
        [DataMember]
        public string placeOfBirth { get; set; }
        [DataMember]
        public string placeOfIssue { get; set; }
        [DataMember]
        public int? requestId { get; set; }
        [DataMember]
        public string sname { get; set; }
        [DataMember]
        public string tname { get; set; }
        [DataMember]
        public string updatedBy { get; set; }
        [DataMember]
        public DateTime? updatedDate { get; set; }
    }
}
