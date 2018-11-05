using JIC.Base.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JIC.Services.ServicesInterfaces
{
    public interface ICircuitConfigurationService
    {
        bool AddCircuitConfiguration(vw_CircuitConfiguration vw_CircuitConfiguration);
    }
}
