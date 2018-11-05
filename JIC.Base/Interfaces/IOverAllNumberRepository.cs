using JIC.Base.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JIC.Base.Interfaces
{
  public interface IOverAllNumberRepository
    {
        int AddOverAllNumber(vw_CrimeCaseBasicData caseBasicData);
        bool AddOverAllNumber(int caseID, out long number, out int prosecutionID, out int year,out int OverAllID);
        int GetOverAllID(long number, int prosecutionID, int year,int CourtID);
        bool IsOverAllEmpty(long number, int prosecutionID, int year);
        void DisableOverAll(int caseID);
    }
}
