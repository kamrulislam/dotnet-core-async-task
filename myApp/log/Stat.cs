using System;
using System.Collections.Generic;

using myApp.myEvent;

namespace myApp.log {

    class Stat {
        public string batchName {get; set;}
        public int numberOfEvents {get; set;}
        public int numberOfCurated {get; set;}
        public int numberOfExceptions {get; set;}

        public double totalProcessTime {get; set;}

        public Dictionary<EventType, int> numberOfEventsBasedOnType {get;}


        public Stat(string batchName) {
            this.numberOfEventsBasedOnType = new Dictionary<EventType, int>();
            this.numberOfEvents = 0;
            this.numberOfCurated = 0;
            this.numberOfExceptions = 0;
            this.batchName = batchName;
            this.totalProcessTime = 0.0;
        }


        public void UpdateStat(IList<MyEvent> events, List<bool> validationResult, double totalProcessTime) {
            int index = 0;
            foreach (var anEvent in events) {
                this.numberOfEvents++;
                if (!this.numberOfEventsBasedOnType.ContainsKey(anEvent.eventType)) {
                    this.numberOfEventsBasedOnType.Add(anEvent.eventType, 1);
                } else {
                    this.numberOfEventsBasedOnType[anEvent.eventType] = (this.numberOfEventsBasedOnType[anEvent.eventType] + 1);
                }

                if (validationResult[index++]) {
                    this.numberOfCurated++;    
                } else {
                    this.numberOfExceptions++;
                }
                
            }
            this.totalProcessTime = totalProcessTime;
        }

        public void toString() {
            var arrivals = this.numberOfEventsBasedOnType[EventType.Arrival];
            var departures = this.numberOfEventsBasedOnType[EventType.Departure];
            Console.WriteLine($"batchName={this.batchName}, numberOfEvents={this.numberOfEvents}, " +
                $"numberOfEventsBasedOnType[Arrival]={arrivals}, " +
                $"numberOfEventsBasedOnType[Departure]={departures}");
        }

    }

}