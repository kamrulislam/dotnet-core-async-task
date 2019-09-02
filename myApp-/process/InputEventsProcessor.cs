using System;
using System.IO;
using System.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;

using myApp.validator;
using myApp.io;
using myApp.process;
using myApp.myEvent;
using myApp.log;

namespace myApp.process {
    class InputEventsProcessor {
        private string file;
        private List<Validator> validators;
        public InputEventsProcessor(string file, List<Validator> validators) {
            this.file = file;
            this.validators = validators;
        }

        public async void Process() {
            var startTime = DateTime.Now;
            ReadFile readFile = new ReadFile();
            String fileData = readFile.ReadFileAsync(this.file).Result;
            FileCopier fileCopier = new FileCopier();
            String lockFileName = this.file.Substring(this.file.LastIndexOf(Path.DirectorySeparatorChar) + 1);
            String fileNameWithoutLock = lockFileName.Substring(0, lockFileName.Length - 5);
            await fileCopier.copy(Config.GetRawPath() + Path.DirectorySeparatorChar + fileNameWithoutLock, fileData );
            JsonToObjectConverter converter = new JsonToObjectConverter();
            var jsonObject = converter.Convert(fileData);
            Stat stat = new Stat(fileNameWithoutLock);
            ValidationApplier validationApplier = new ValidationApplier();
            List<bool> validationResult = validationApplier.Validate(jsonObject, this.validators);
            EventSeparator eventSeparator = new EventSeparator();
            eventSeparator.Separate(jsonObject, validationResult);
            var endTime = DateTime.Now;
            Double elapsedMilliseconds = ((TimeSpan)(endTime - startTime)).TotalMilliseconds;
            stat.UpdateStat(jsonObject, validationResult, elapsedMilliseconds);
            EventLog log = new EventLog();
            Console.WriteLine(log.Generate(stat));
            Console.WriteLine("---------------------------------");
            File.Delete(file);
        }
    }
}