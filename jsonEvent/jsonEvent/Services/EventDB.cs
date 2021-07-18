using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace jsonEvent.Services
{
    public class EventDB : IEventDB
    {
        private List<Event> _events  = new List<Event>();
        public List<Event> Events => _events;
        public void AddEvent(Event ev)
        {
            _events.Add(ev);
        }
        public List<Event> EventsAfterTime(DateTime time)
        {
            var answer = new List<Event>();
            for(int i = _events.Count - 1; i >= 0 && _events[i].Time > time; i--)
            {
                answer.Add((Event)_events[i].Clone());
            }
            return answer;
        }
    }
}
