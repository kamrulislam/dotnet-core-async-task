using System;
using Xunit;
using System.Collections.Generic;


using myApp.validator;
using myApp.myEvent;

namespace myAppTests
{
    public class PassengerValidatorTest
    {
        [Theory]
        // [MemberData(nameof(TestDataGenerator.GetEventDataGenerator), MemberType = typeof(TestDataGenerator))]
        [MemberData(nameof(MyEventPassengerValues))]
        public void ValidatePassengerMeetsMinRequirement(MyEvent myEvent, bool expected)
        {
            var passengerValidator = new PassengerValidator();
            var result = passengerValidator.Validate(myEvent);

            Assert.Equal(expected, result);
        }


        public static IEnumerable<object[]> MyEventPassengerValues()
        {
            yield return new object[] { new MyEvent {passengers = 40}, true };
            yield return new object[] { new MyEvent {passengers = 39}, false };
        }
    }
}
