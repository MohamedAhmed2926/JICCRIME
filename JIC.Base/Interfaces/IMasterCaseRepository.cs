using JIC.Base.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JIC.Base.Interfaces
{
    public interface IMasterCaseRepository
    {
        CaseSaveStatus AddCaseBasicDate(vw_CaseBasicData caseBasicData , out int MasterCaseID);
        void UpdateCaseBasicData(int caseID, vw_CaseBasicData caseBasicData);
        void DeleteCaseBasicData( int MasterCaseID);
        IQueryable<vw_unCompletCase> UnCompletCase(int courtId);
        bool IsCaseConnectedToCircuit(int CircuitID);
        void SetCaseCompleted(int caseID);
        void AddOverAll(int caseID, int overAllID);
        bool IsSecondLevelNumberExistBefore(int SecondNumber, int SecondYear, int ProsecutionID,int MasterCaseID);
    }
}
