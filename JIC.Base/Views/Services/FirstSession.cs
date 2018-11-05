using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace JIC.Base.Views.Services
{
    [DataContract]
    public class CaseSession
    {
        private DateTime sessionDate;

        [DataMember]
        public DateTime Session_Date
        {
            get { return sessionDate; }
            set { sessionDate = value.ToLocalTime().Date; }
        }

        [DataMember]
        public int Circuit_ID { get; set; }
        [DataMember]
        public int Reservation_Code { get; set; }
        public int Court_ID { get; private set; }
        public int CaseTypeID { get; private set; }

        internal void SetCourtID(int court_id)
        {
            this.Court_ID = court_id;
        }
        internal void SetCaseTypeID(int case_type_id)
        {
            this.CaseTypeID = case_type_id;
        }

    }
}
