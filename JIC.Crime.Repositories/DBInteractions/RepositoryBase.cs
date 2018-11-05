using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JIC.Crime.Repositories.DBInteractions
{
    public abstract class RepositoryBase
    {
        #region Properties
        protected JICCrimeContext DataContext { get { return DBFactory.Get(); } }
        #endregion
    }
}
