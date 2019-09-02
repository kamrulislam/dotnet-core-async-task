using System;
using myApp.myEvent;

namespace myApp.validator {
    class DelayValidator : Validator {
        public TimeSpan MaxDelay = new TimeSpan(3);
        public override bool Validate(MyEvent myEvent) {
            if (TimeSpan.Compare(myEvent.delayed, MaxDelay) == 1) {
                return false;
            }
            return true;
        }
    }
}