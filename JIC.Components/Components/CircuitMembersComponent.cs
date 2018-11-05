using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JIC.Base;
using JIC.Base.Views;
using JIC.Base.Interfaces;

namespace JIC.Components.Components
{
    public class CircuitMembersComponent
    {
        ICircuitMembersRepository CircuitMembersRepository;

        public CircuitMembersComponent( ICircuitMembersRepository CircuitMembersRepository)
        {
            this.CircuitMembersRepository = CircuitMembersRepository;
        }

        public bool IsPersonIsCircuitMember(int PersonID, int CaseID)
        { return CircuitMembersRepository.IsPersonIsCircuitMember( PersonID,  CaseID); }
        public bool IsCircuitMember(int userID)
        {
            return CircuitMembersRepository.IsCircuitMember(userID);
        }
        public SaveCircuitStatus AddCircuitJudges(List<vw_CircuitsJudges> JudgesList ,int CircuitID,DateTime CircuitStartDate)
        {

            return CircuitMembersRepository.AddCircuitJudges(JudgesList,CircuitID,CircuitStartDate);
        }
        public SaveCircuitStatus EditCircuitJudges(List<vw_CircuitsJudges> JudgesList, int CircuitID, DateTime CircuitStartDate)
        {

            return CircuitMembersRepository.EditCircuitJudges(JudgesList, CircuitID, CircuitStartDate);
        }
        public DeleteStatus DeleteCircuitMemberByCircuitID(int CircuitID)
        {
            return CircuitMembersRepository.DeleteMembersByCircuitID( CircuitID);
        }
        public List<vw_CircuitsJudges>  GetCircuitMembersByCircuitID(int CircuitID)
        {
            return CircuitMembersRepository.GetCircuitMembersByCircuitID (CircuitID);
        }
    }
}
