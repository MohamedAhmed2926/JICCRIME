using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JIC.Base;
using JIC.Components.Components;
using JIC.Services.ServicesInterfaces;
using JIC.Base.Views;

namespace JIC.Services.Services
{
    public class OrderOfAssignmentService : ServiceBase , IOrderOfAssignment
    {
        public OrderOfAssignmentService(CaseType caseType) : base(caseType)
        {
        }

        OrderOfAssignmentComponent OrderOfAssignmentComponent { get { return GetComponent<OrderOfAssignmentComponent>(); } }

        public AddOrder AddOrder(vw_OrderOfAssignment _OrderOfAssignment)
        {
            return OrderOfAssignmentComponent.AddOrder(_OrderOfAssignment);
        }

        public EditAssignment EditOrder(vw_OrderOfAssignment _OrderOfAssignment)
        {
            return OrderOfAssignmentComponent.EditOrder(_OrderOfAssignment);
        }

        public vw_OrderOfAssignment GetOrderByID(int CaseID)
        {
            return OrderOfAssignmentComponent.GetOrderByID(CaseID);
        }
    }
}
