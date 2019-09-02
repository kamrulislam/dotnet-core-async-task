using System;
using System.IO;

namespace myApp {
    class Config{
        public  const string ROOT_FOLDER = "working-directory";
        public  const string INPUT_FOLDER = "input";
        public  const string RAW_FOLDER = "raw";

        public  const string CURATED_FOLDER = "curated";
        public  const string EXCEPTION_FOLDER = "exception";

        public static string GetWorkingDirectory() {
            return Directory.GetCurrentDirectory() + 
                    Path.DirectorySeparatorChar + 
                    ROOT_FOLDER;
        }
        public static string GetInputPath() {
            return GetWorkingDirectory() + 
                    Path.DirectorySeparatorChar + 
                    INPUT_FOLDER;
        }

        public static string GetRawPath() {
            return GetWorkingDirectory() + 
                    Path.DirectorySeparatorChar + 
                    RAW_FOLDER;
        }
        public static string GetCuratedPath() {
            return GetWorkingDirectory() + 
                    Path.DirectorySeparatorChar + 
                    CURATED_FOLDER;
        }

        public static string GetExceptionPath() {
            return GetWorkingDirectory() + 
                    Path.DirectorySeparatorChar + 
                    EXCEPTION_FOLDER;
        }

    }
}