using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JIC.Base.Views;

namespace JIC.Services.ServicesInterfaces
{
   public interface IPersonInformationService
    {
      vw_InformationPerson  GetInformationPerson(string NatNo, string Name);
    }
}
