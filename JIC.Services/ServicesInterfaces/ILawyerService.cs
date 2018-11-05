using JIC.Base;
using System;
using JIC.Base.views;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JIC.Base.Views;

namespace JIC.Services.ServicesInterfaces
{
    public interface ILawyerService
    {
        LawyerStatus AddLawyer(vw_LawyerData lawyerData, out int LawyerID);
        List<vw_LawyerData> GetLawyers();
        vw_LawyerData GetLawyerByID(int? LawyerID);
        LawyerStatus EditLawyer(vw_LawyerData Lawyer);

    }
}
