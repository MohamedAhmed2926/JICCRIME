using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace JIC.Base.Views.Services
{
    [DataContract]
    public class CaseBase
    {
        [DataMember]
        public int Business_Case_Id { get; set; }
        [DataMember]
        public int First_Case_No { get; set; }
        [DataMember]
        public int First_Case_Year { get; set; }
        [DataMember]
        public int First_Case_Police_Station_ID { get; set; }
        [DataMember]
        public int Court_ID { get; set; }
        
        [DataMember]
        public CaseDescription CaseDescription { get; set; }
        [DataMember]
        public int CrimeID { get; set; }
        [DataMember]
        public int ProcedureTypeID { get; set; }
        [DataMember]
        public int CaseTypeID { get; set; }
        [DataMember]
        public List<CaseParty> CaseParties { get; set; }
        [DataMember]
        public List<Document> Documents { get; set; }

        #region HelperMapping
        public vw_CaseDescription MapCaseDescription(int CaseID)
        {
            return new vw_CaseDescription
            {
                Description = this.CaseDescription.Description,
                LawItems = this.CaseDescription.LawItems,
                CaseID = CaseID
            };
        }
        #endregion
    }
}
