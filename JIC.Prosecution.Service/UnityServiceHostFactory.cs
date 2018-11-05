using JIC.Base;
using JIC.Base.Interfaces;
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

namespace JIC.Prosecution.Service
{
    public class UnityServiceHostFactory : ServiceHostFactory
    {
        private readonly IUnityContainer container;

        public UnityServiceHostFactory()
        {
            container = new UnityContainer();
            RegisterTypes(container);
        }

        protected override ServiceHost CreateServiceHost(
          Type serviceType, Uri[] baseAddresses)
        {
            return new UnityServiceHost(this.container,
              serviceType, baseAddresses);
        }

        protected virtual void RegisterTypes(IUnityContainer container)
        {
            JIC.Services.ServiceUnityContainer.RegisterTypes(container);
        }

        protected void RegisterTypes(CaseType caseType)
        {
            container.RegisterInstance(caseType);
            RepositoryFactory.InitializeContainer(container, caseType);
            JIC.Services.ServiceUnityContainer.InitializeMapper(RepositoryFactory.GetMapperProfiles(caseType));
        }

        public T Resolve<T>()
        {
            return container.Resolve<T>();
        }
    }

}