using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Unity;

namespace JIC.Services.EventHandler
{
    public class EventHandler
    {
        private IUnityContainer unityContainer;
        private IEnumerable<Type> Events { get; set; } 
        public EventHandler(IUnityContainer unityContainer)
        {
            this.unityContainer = unityContainer;
            //This will search all the Assemblies for the EventHandler
            //But will be done only once through the Initialization
            //Automatic Discovery Of All EventHandler
            //No Need for Definitions Any More
            //Ahmed Ghorab 03-02-2018
            //-----7-2 -------
            //AG Searching in All Domains Failed On one PC
            //Moved to Searching in Service Domain To Solve This Issue
            //Events = AppDomain.CurrentDomain.GetAssemblies()
            //    .SelectMany(s => s.GetTypes())
            //    .Where( p => typeof(IEventBus).IsAssignableFrom(p) && !p.IsAbstract && p.IsClass).ToList();
            //--------------------
            Events = this.GetType().Assembly.GetTypes().Where(p => typeof(IEventBus).IsAssignableFrom(p) && !p.IsAbstract && p.IsClass).ToList();
        }

        public void Event(object obj)
        {
            //Get EventBus Generic Type Fullname 
            //Every type that implement This will have the Name at the Start of the Implementation
            //Also this will fix the Bug (it didn't happen :D)
            //If there was an Generic interface that implement the Event but not an event handler 
            //so the Handle Function will be called and this shouldn't happen
            var EventBusFullName = typeof(IEventBus<>).FullName;
            //Search For Event Handler that Waiting For This Event
            //Seach For Interfaces Type IEventBus<T>
            //This will get the T and search if any of them has the Needed Function
            //This will allow the Handler to Handle multiple Event as Once 
            //Ahmed Ghorab 03-02-2018
            var HandlerEvents = Events.Where(_event => _event.GetInterfaces().Any(x =>
                x.FullName.StartsWith(EventBusFullName) && x.IsGenericType && x.GenericTypeArguments.Any(_type=>_type == obj.GetType())
            ));
            foreach (var _event in HandlerEvents)
            {
                var EventHandler = unityContainer.Resolve(_event);
                //Call Handle Function Dynamiclly 
                //Since i can't cast it to Generic Type 
                //EventHandler.Handle(obj);
                //Ahmed Ghorab 03-02-2018
                _event.GetMethod("Handle").Invoke(EventHandler, new object[] { obj });
            }
        }
    }
}
