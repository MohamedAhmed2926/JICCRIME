using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace JIC.Base
{
    public static class Extensions
    {
        public static long Code(this CaseLevels caseLevels)
        {
            switch (caseLevels)
            {
                case CaseLevels.Initial:
                    return 01;
                case CaseLevels.Elementary:
                    return 02;
                case CaseLevels.Cassation:
                    return 03;
                default:
                    return 01;
            }
        }
    }
}
