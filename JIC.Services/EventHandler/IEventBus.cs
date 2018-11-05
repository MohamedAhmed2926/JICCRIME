using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JIC.Services.EventHandler
{
    public interface IEventBus
    {
    }
    public interface IEventBus<T> : IEventBus
    {
        void Handle(T handle);
    }
}
