using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JIC.Crime.View.Models
{
    public class PersonDataViewModels
    {
        public string Name { get; set; }
        public string NatNo { get; set; }
        public int NationalityID { get; set; }
        public string PassportNo { get; set; }
        public string Job { get; set; }
        public AddressViewModels address { get; set; }
        public DateTime BirthDate { get; set; }
    }
}
