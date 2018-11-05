using JIC.Base.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JIC.Base.Interfaces
{
    public interface ICircuitMembersRepository
    {
        bool IsCircuitMember(int userID);
        SaveCircuitStatus AddCircuitJudges(List<vw_CircuitsJudges> JudgesList, int CircuitID, DateTime CircuitStartDate);
        SaveCircuitStatus EditCircuitJudges(List<vw_CircuitsJudges> JudgesList, int CircuitID, DateTime CircuitStartDate);
         bool IsPersonIsCircuitMember(int PersonID, int CaseID);
        DeleteStatus DeleteMembersByCircuitID(int CircuitID);
        List<vw_CircuitsJudges> GetCircuitMembersByCircuitID(int CircuitID);
    }
}
