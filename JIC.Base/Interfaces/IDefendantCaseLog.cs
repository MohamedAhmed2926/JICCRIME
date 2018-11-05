using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JIC.Base.Interfaces
{
    public interface IDefendantCaseLogRepository
    {
        void AddDefendantCaseLog(long defendantID, int defendantStatus, DateTime FromDate);
        void EditDefendantCaseLog(long defendantID, int defendantStatus, DateTime FromDate);
        void DeleteDefendantCaseLog(long defendantID);
    }
}
