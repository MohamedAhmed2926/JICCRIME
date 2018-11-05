using JIC.Base;
using JIC.Components;
using JIC.Components.Components;
using JIC.Services.EventHandler;
using JIC.Services.Handler;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Unity;

namespace JIC.Services
{
    public abstract class ServiceBase
    {
        private CaseType caseType;
        private static Dictionary<CaseType, IUnityContainer> DPContainer = new Dictionary<CaseType, IUnityContainer>();
       
        public ServiceBase(CaseType caseType)
        {
            this.caseType = caseType;
            InitializeDPContainer(caseType);
        }

        public void Event(object obj)
        {
            //new EventHandler.EventHandler(GetDPContainer()).Event(obj);
            GetComponent<EventHandler.EventHandler>().Event(obj);
        }
        #region Methods

        private void InitializeDPContainer(CaseType caseType)
        {
            if (!DPContainer.ContainsKey(caseType))
            {
                IUnityContainer unityContainer = new UnityContainer();
                unityContainer.RegisterInstance(caseType);
                ComponentFactory.InitializeContainer(caseType, unityContainer);
                unityContainer.RegisterInstance(new EventHandler.EventHandler(unityContainer));
                DPContainer[caseType] = unityContainer;
            }
                
        }

        //protected ComponentFactory ComponentFactory
        //{
        //    get
        //    {
        //        return ComponentFactory.GetInstance(caseType);
        //    }
        //}

        public void HandleException(Exception ex)
        {
            Log.GetLogger().LogException(ex);
        }

        public T GetComponent<T>()
        {
            return GetDPContainer().Resolve<T>();
        }
        private IUnityContainer GetDPContainer()
        {
            return DPContainer[caseType];
        }
        public DbContextTransaction BeginDatabaseTransaction()
        {
            return GetComponent<DatabaseComponent>().BeginTransaction();
        }

        #endregion
    }
}
