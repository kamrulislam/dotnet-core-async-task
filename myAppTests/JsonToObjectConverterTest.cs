using System;
using Xunit;
using System.Collections;
using System.Collections.Generic;

using myApp.process;

namespace myAppTest {
    public class JsonToObjectConverterTest {
        [Theory]
        [InlineData("[{\"eventType\":\"Departure\",\"timeStamp\":\"2019-07-13T11:07:52.106Z\",\"flight\":\"NZ732\",\"destination\":\"Melbourne\",\"passengers\":\"115\"}]", 1)]
        public void ConvertsCorrectNumberOfEvents(string jsonString, int numberOfObjects) {
            JsonToObjectConverter jsonToObjectConverter = new JsonToObjectConverter();
            var result = jsonToObjectConverter.Convert(jsonString);
            Assert.Equal(numberOfObjects, result.Count);
        }
    }
}