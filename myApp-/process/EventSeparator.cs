using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.IO;

using myApp.io;
using myApp.myEvent;

namespace myApp.process {
    class EventSeparator {

        private string generateFolderNameForEvent(bool isValid) {
            if (isValid) {
                return Config.GetCuratedPath();
            } else {
                return Config.GetExceptionPath();
            }
        }
        private string generateFileNameForEvent(MyEvent myEvent, bool isValid) {
            return string.Format(generateFolderNameForEvent(isValid) + Path.DirectorySeparatorChar + "{0}-{1}.json", myEvent.eventType.ToString(), myEvent.startTicks);
        }
        public void Separate(List<MyEvent> myEvents, List<bool> validationResult) {
            int index = 0;
            List<Task> copiers = new List<Task>();
            foreach(var myEvent in myEvents) {
                copiers.Add(new FileCopier().copy(this.generateFileNameForEvent(myEvent, validationResult[index++]), JsonConvert.SerializeObject(myEvent)));                
            }
            Task.WaitAll(copiers.ToArray());
        }
    }
}