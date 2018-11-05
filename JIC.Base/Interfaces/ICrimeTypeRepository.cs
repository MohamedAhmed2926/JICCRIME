using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JIC.Base.Interfaces
{
  public  interface ICrimeTypeRepository
    {
        IQueryable<Views.vw_KeyValue> GetCrimType(int? CourtID);
    }
}
