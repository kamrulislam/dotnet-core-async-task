using System;

namespace myApp.myEvent {

    public class MyEvent {
        public EventType eventType { get; set; }
        public DateTime timeStamp { get; set; }
        public string flight { get; set; }
        public string destination { get; set; }
        public int passengers { get; set; }
        public TimeSpan delayed { get; set; }
        public long startTicks {get; set;}

        public MyEvent() {
            this.startTicks = DateTime.Now.Ticks;
        }
        public static EventType ConvertEventType(String eventType) {
            return eventType.Equals("Arrival") ? EventType.Arrival : EventType.Departure;
        }
    }
}