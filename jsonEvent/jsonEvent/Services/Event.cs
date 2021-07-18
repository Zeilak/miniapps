using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace jsonEvent.Services
{
    public class Event : ICloneable
    {
        public string Name { get; set; }
        public int Value { get; set; }
        public DateTime Time { get; set; }
        public Event() { }
        public Event(string name, int value, DateTime time)
        {
            Name = name;
            Value = value;
            Time = time;
        }
        public object Clone()
        {
            Event ev = new Event() { Name = new String(this.Name), Value = this.Value, Time = this.Time };
            return ev;
        }
    }
}
