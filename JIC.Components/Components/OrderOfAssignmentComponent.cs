using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JIC.Base;
using JIC.Base.Interfaces;
using JIC.Base.Views;

namespace JIC.Components.Components
{
    public class OrderOfAssignmentComponent
    {
        private IOrderOfAssignmentRepository IOrderOfAssignmentRepository;
        public OrderOfAssignmentComponent( IOrderOfAssignmentRepository IOrderOfAssignmentRepository)
        {
            this.IOrderOfAssignmentRepository = IOrderOfAssignmentRepository;
        }

        public AddOrder AddOrder(vw_OrderOfAssignment OrderOfAssignment)
        {
            return IOrderOfAssignmentRepository.AddOrder(OrderOfAssignment);
        }
        public vw_OrderOfAssignment GetOrderByID(int CaseID)
        {
            return IOrderOfAssignmentRepository.GetOrderByID(CaseID);
        }
        public EditAssignment EditOrder(vw_OrderOfAssignment OrderOfAssignment)
        {
            return IOrderOfAssignmentRepository.EditOrder(OrderOfAssignment);
        }

        public bool IsValid(int caseID)
        {
            return !String.IsNullOrEmpty(IOrderOfAssignmentRepository.GetOrderByID(caseID).Description);
        }
    }
}
