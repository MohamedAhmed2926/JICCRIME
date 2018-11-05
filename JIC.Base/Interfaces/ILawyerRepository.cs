using JIC.Base.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JIC.Base.Interfaces
{
    public interface ILawyerRepository
    {
        LawyerStatus AddLawyer(vw_LawyerData lawyerData, out int LawyerID);
        List<vw_LawyerData> GetLawyers();
        vw_LawyerData GetLawyerByID(int? LawyerID);
        LawyerStatus EditLawyer(vw_LawyerData Lawyer);
     
    }
}
