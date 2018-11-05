using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JIC.Fault.Repositories.DBInteractions
{
    public abstract class RepositoryBase
    {
        #region Properties
        protected JICFaultContext DataContext { get { return DBFactory.Get(); } }
        #endregion
    }
}
