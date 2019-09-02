using myApp.myEvent;

namespace myApp.validator { 
    public class PassengerValidator : Validator {
        public const int MinPassengers = 40;
        public override bool Validate(MyEvent myEvent) {
            if (myEvent.passengers < MinPassengers) {
                return false;
            }
            return true;
        }
    }
}