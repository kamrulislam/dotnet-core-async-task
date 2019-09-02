using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

using myApp.myEvent;

namespace myApp.process {
    public class JsonToObjectConverter {
        public List<MyEvent> Convert(string json) {
            JArray eventsArr = JArray.Parse(json);
            List<MyEvent> myEvents = new List<MyEvent>();
            foreach(var eventDetail in eventsArr.Children()) {
                var myEvent = new MyEvent();
                myEvent.eventType = MyEvent.ConvertEventType((string)eventDetail["eventType"]);
                myEvent.timeStamp = (DateTime)eventDetail["timeStamp"];
                myEvent.flight = (string)eventDetail["flight"];
                myEvent.passengers = (int)eventDetail["passengers"];
                if (myEvent.eventType.Equals(EventType.Arrival)) {
                    myEvent.delayed = (TimeSpan)eventDetail["delayed"];
                } 
                if (myEvent.eventType.Equals(EventType.Departure)) {
                    myEvent.destination = (string) eventDetail["destination"];
                }
                myEvents.Add(myEvent);
            }
            return myEvents;
        }
    }
}