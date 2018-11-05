using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Web;
using Unity;

namespace JIC.Prosecution.Service
{
    public class UnityServiceHost : ServiceHost
    {
        public UnityServiceHost(IUnityContainer container,
          Type serviceType, params Uri[] baseAddresses)
          : base(serviceType, baseAddresses)
        {
            if (container == null)
            {
                throw new ArgumentNullException("container");
            }

            foreach (var cd in this.ImplementedContracts.Values)
            {
                cd.Behaviors.Add(new UnityInstanceProvider(container));
            }
        }
    }
}