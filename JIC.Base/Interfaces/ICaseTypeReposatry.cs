using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JIC.Base.Interfaces
{
  public  interface ICaseTypeReposatry
    {
        IQueryable<JIC.Base.Views.vw_KeyValue> GetCaseType();
    }
}
