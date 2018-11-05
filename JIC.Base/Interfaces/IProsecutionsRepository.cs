using JIC.Base.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JIC.Base.Interfaces
{
   public  interface IProsecutionsRepository
    {
       IQueryable< vw_KeyValue> GetProsecution(int? courtId);
        List<vw_KeyValue> GetProsecutionFromPros(int prosecution);
        List<vw_KeyValue> GetInitialProsInCourt(int courtID);
        List<vw_KeyValue> GetElementaryProsecutions(int courtID);
    }
}
