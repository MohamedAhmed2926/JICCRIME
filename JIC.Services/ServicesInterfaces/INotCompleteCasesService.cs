using JIC.Base.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JIC.Services.ServicesInterfaces
{
   public interface INotCompleteCasesService
    {
       IQueryable<vw_unCompletCase> GetNotCompleteCase(int CourtId);
       bool DeleteNotCompleteCase(int CaseID);

    }
}
