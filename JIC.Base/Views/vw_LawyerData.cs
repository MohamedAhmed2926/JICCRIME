using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JIC.Base.Views
{
    public class vw_LawyerData
    {
        public int ID { get; set; }

        public int LawyerLevelID { get; set; }

        public long PersonID { get; set; }

        public string LawyerName { get; set; }

        public string NationalID { get; set; }

        public string LawyerLevelName { get; set; }
        public string LawyerCardNumber { get; set; }

        public DateTime? DateOfBirth  { get; set; }

        public string Address { get; set; }

        public byte[] LawyerFileData { get; set; }


    }
}
