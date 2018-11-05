using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JIC.Base
{
    public interface IValidation
    {
        ValidationExceptions valid();
    }

    public interface ISaved
    {
        bool IsSaved();
        string getTitle();
    }
}
