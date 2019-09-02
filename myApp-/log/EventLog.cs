
using System;

using myApp.myEvent;

namespace myApp.log {

    class EventLog {

        public string Generate(Stat stat) {
            // var eventTypes = Enum.GetValues(typeof(EventType));  
            string s = "Batch Name: {0}\n";
            s += "Total number of events: {1}";
            
            EventType[] eventTypes = new EventType[] { EventType.Arrival, EventType.Departure };
            string msg = string.Format(s, stat.batchName, stat.numberOfEvents);  
            foreach(var eventType in eventTypes) {
                msg += "\nNumber of events for " + eventType + ":" + stat.numberOfEventsBasedOnType[eventType];
            }
            msg += "\nNumber of Curated: " + stat.numberOfCurated;
            msg += "\nNumber of Exceptions: " + stat.numberOfExceptions;
            msg += "\nTotal execution time (in milliseconds): " + stat.totalProcessTime;

            return msg;
        }

    }
}