using JIC.Services.ServicesInterfaces;
using JIC.Components.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JIC.Base.Views;
using JIC.Base;

namespace JIC.Services.Services
{
   public class CircuitMembersService: ServiceBase, ICircuitMembersService
    {
        private CircuitMembersComponent CircuitMembersComp { get { return GetComponent<CircuitMembersComponent>(); } }
        public CircuitMembersService(CaseType caseType) : base(caseType)
        {
        }

        public List<vw_CircuitsJudges> GetCircuitMembersByCircuitID(int CircuitID)
        {
           return CircuitMembersComp.GetCircuitMembersByCircuitID(CircuitID);
        }
    }
}
