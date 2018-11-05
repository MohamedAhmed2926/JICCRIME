using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JIC.Base;
using JIC.Base.Views;
namespace JIC.Services.ServicesInterfaces
{
   public interface IOrderOfAssignment
    {
        AddOrder AddOrder(vw_OrderOfAssignment _OrderOfAssignment);
        vw_OrderOfAssignment GetOrderByID(int CaseID);
        EditAssignment EditOrder(vw_OrderOfAssignment _OrderOfAssignment);

    }
}
