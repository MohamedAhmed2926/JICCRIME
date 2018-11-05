using JIC.Base;
using JIC.Repositories;
using JIC.Services.Services;
using JIC.Services.ServicesInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.Web;
using Unity;

namespace JIC.Prosecution.Service.Fault
{
    public class FaultUnityServiceHostFactory : UnityServiceHostFactory
    {
        protected override void RegisterTypes(IUnityContainer container)
        {
            RegisterTypes(CaseType.Fault);            
            base.RegisterTypes(container);
        }
    }
   

}