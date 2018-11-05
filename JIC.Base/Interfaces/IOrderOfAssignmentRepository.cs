using JIC.Base.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JIC.Base.Interfaces
{
    public interface IOrderOfAssignmentRepository
    {
        AddOrder AddOrder(vw_OrderOfAssignment OrderOfAssignment);
        vw_OrderOfAssignment GetOrderByID(int CaseID);
        EditAssignment EditOrder(vw_OrderOfAssignment OrderOfAssignment);
    }
}
