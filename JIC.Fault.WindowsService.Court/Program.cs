using JIC.Base;
using JIC.Repositories;
using Quartz.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using Unity;

namespace JIC.Fault.WindowsService.Court
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            IUnityContainer container = GetDIContainer();

            ServiceBase[] ServicesToRun = new ServiceBase[]
            {
                container.Resolve<SchedularService>()
            };
            ServiceBase.Run(ServicesToRun);
        }

        private static IUnityContainer GetDIContainer()
        {
            var container = new UnityContainer();
            container.RegisterInstance(CaseType.Fault);
            RepositoryFactory.InitializeContainer(container, CaseType.Fault);
            JIC.Services.ServiceUnityContainer.RegisterTypes(container);
            container.AddNewExtension<QuartzUnityExtension>();
            return container;
        }
    }
}
