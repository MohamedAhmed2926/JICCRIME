using JIC.Base.Views.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace JIC.Prosecution.Service.Fault
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IFaultCourtService" in both code and config file together.
    [ServiceContract]
    public interface IFaultCourtService
    {
        [OperationContract]
        Response AddNewCase(NewCase Case);

        [OperationContract]
        Response AddCompleteCase(CompleteCase Case);

        [OperationContract]
        Response RequestObjection(ObjectionRequest objectionRequest);

        [OperationContract]
        Response RequestResumption(ResumptionRequest resumptionRequest);
    }


}
