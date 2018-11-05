using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using JIC.Base;
using JIC.Services.ServicesInterfaces;
using JIC.Base.Views;

namespace JIC.Crime.View.TestService
{
    public class OrderOfAssignmentService : IOrderOfAssignment
    {
        public AddOrder AddOrder(vw_OrderOfAssignment _OrderOfAssignment)
        {
            return Base.AddOrder.FailedToAdd;
        }

        public EditAssignment EditOrder(vw_OrderOfAssignment _OrderOfAssignment)
        {
            return EditAssignment.EditSuccefull;
        }

        public vw_OrderOfAssignment GetOrderByID(int CaseID)
        {
            vw_OrderOfAssignment vw_OrderOf = new vw_OrderOfAssignment();
            vw_OrderOf.CaseID = 5;
            vw_OrderOf.Description ="فغيبسبلبغي";
            return vw_OrderOf;
        }
    }
}