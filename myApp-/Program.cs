using System;
using System.Threading;
using System.Collections;
using System.Collections.Generic;

using myApp.validator;
using myApp.io;
using myApp.process;

namespace myApp
{
    class Program
    {
        static void Main(string[] args)
        {
            ReadDir readDir = new ReadDir();
            List<Validator> validators = new List<Validator>();
            validators.Add(new PassengerValidator());
            validators.Add(new DelayValidator());
            while(true) {
                string file = readDir.FindNextFile(Config.GetInputPath());
                if (file != null) {
                    InputEventsProcessor inputEventsProcessor = new InputEventsProcessor(file, validators);
                    inputEventsProcessor.Process();
                } else {
                    Console.WriteLine("nothing to process");
                }
                Thread.Sleep(100);
            }
        }
    }
}
