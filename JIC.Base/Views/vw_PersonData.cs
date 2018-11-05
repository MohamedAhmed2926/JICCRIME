using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JIC.Base.Views
{
    public class vw_PersonData
    {
        public long ID { get; set; }
        public string Name { get; set; }
        public string NatNo { get; set; }
        public int? NationalityID { get; set; }
        public string PassportNo { get; set; }
       
        public string Job { get; set; }
        public vw_Address address { get; set; }
        public DateTime? BirthDate { get; set; }
        public string CleanFullName { get; set; }
        public string PhoneNo { get; set; }
        public int Age { get;set; }
    }
}
