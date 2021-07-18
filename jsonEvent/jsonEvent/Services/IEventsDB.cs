using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace jsonEvent.Services
{
    /// <summary>
    /// тут хранятся события
    /// еще можно вызвать поиск в хранилище
    /// </summary>
    public interface IEventDB
    {
        List<Event> Events { get; }
        List<Event> EventsAfterTime(DateTime time);
        void AddEvent(Event ev);
    }
}
