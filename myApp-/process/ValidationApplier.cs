using System.Collections;
using System.Collections.Generic;

using myApp.validator;
using myApp.myEvent;

namespace myApp.process {
    class ValidationApplier {
        private List<bool> result;

        public ValidationApplier() {
            result = new List<bool>();
        }

        public List<bool> Validate(List<MyEvent> myEvents, List<Validator> validators) {
            foreach(var myEvent in myEvents) {
                var isSatisfied = true;
                foreach(var validator in validators) {
                    isSatisfied = isSatisfied && validator.Validate(myEvent);
                }
                this.result.Add(isSatisfied);
            }
            return result;
        }
    }
}