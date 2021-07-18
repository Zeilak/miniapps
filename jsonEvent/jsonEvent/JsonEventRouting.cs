using jsonEvent.Services;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace jsonEvent
{
    /// <summary>
    /// обработка запросов add - добавить событие и check - проверить события
    /// </summary>
    public class JsonEventRouting
    {
        /// <summary>
        /// этот класс нужен для парсинга json
        /// </summary>
        private class EventForm
        {
            public string name { get; set; }
            public int value { get; set; }
            public EventForm() { }
            public EventForm(Event ev)
            {
                name = ev.Name;
                value = ev.Value;
            }
        }
        /// <summary>
        /// этот класс нужен для создания json
        /// </summary>
        private class JsonResponseForm
        {
            public JsonResponseForm(DateTime FromTime, DateTime ToTime, List<EventForm> Events)
            {
                fromTime = FromTime;
                toTime = ToTime;
                events = Events;
            }
            public DateTime fromTime { get; set; }
            public DateTime toTime { get; set; }
            public List<EventForm> events { get; set; }
        }
        private readonly RequestDelegate _next;
        private IEventDB _eventDB;
        public JsonEventRouting(RequestDelegate next, IEventDB eventDB)
        {
            _next = next;
            _eventDB = eventDB;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            string path = context.Request.Path.Value.ToLower();
            if (path == "/add") // обработка json и запись в бд
            {
                var jsonString = String.Empty;
                using (var inputStream = new StreamReader(context.Request.Body))
                {
                    jsonString = await inputStream.ReadToEndAsync();
                }
                EventForm evForm = JsonSerializer.Deserialize<EventForm>(jsonString);
                if(evForm.name == null)
                {
                    context.Response.StatusCode = 400;
                }
                else
                {
                    Event ev = new Event(evForm.name, evForm.value, DateTime.Now);
                    _eventDB.AddEvent(ev);
                    await context.Response.WriteAsync("ok");
                }
            }
            else if (path == "/check") // метод проверки событий за минуту
            {
                DateTime timeNow = DateTime.Now;
                DateTime timeNowBuff = timeNow;
                DateTime timeLater = timeNowBuff.AddMinutes(-1.0);
                List<Event> evs = _eventDB.EventsAfterTime(timeLater);
                await context.Response.WriteAsync(EventListToJson(evs, ref timeLater, ref timeNow));
            }
            else
            {
                context.Response.StatusCode = 404;
            }
            await _next.Invoke(context);
        }

        /// <summary>
        /// создает json, причем value событий складываются, а события не повторяются
        /// </summary>
        /// <param name="evs"></param>
        /// <param name="timeFrom"></param>
        /// <param name="timeTo"></param>
        /// <returns></returns>
        private string EventListToJson(List<Event> evs, ref DateTime timeFrom, ref DateTime timeTo)
        {
            List<EventForm> evforms = new List<EventForm>();
            bool newName = true;
            for(int i = 0; i < evs.Count; i++)
            {
                newName = true;
                for(int j = 0; j < evforms.Count; j++)
                {
                    if(evforms[j].name == evs[i].Name)
                    {
                        newName = false;
                        evforms[j].value += evs[i].Value;
                        break;
                    }
                }
                if(newName)
                    evforms.Add(new EventForm(evs[i])); 
            }
            
            return JsonSerializer.Serialize(new JsonResponseForm(timeFrom, timeTo, evforms));
        }
    }
}
