using myApp.myEvent;

namespace myApp.validator {
    public abstract class Validator {
        abstract public bool Validate(MyEvent myEvent);
    }
}