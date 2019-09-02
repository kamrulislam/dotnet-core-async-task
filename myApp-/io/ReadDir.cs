using System;
using System.Threading.Tasks;
using System.IO;
using System.Collections;
using System.Collections.Generic;

namespace myApp.io {
    class ReadDir{    
        public string FindNextFile(string dirPath) {
            try {
                List<string> files = new List<string>(Directory.EnumerateFiles(dirPath));
                foreach (var file in files) {
                    if (!file.EndsWith(".lock")) {
                        string newFileName = file + ".lock";
                        File.Move(file, newFileName);
                        return newFileName;
                    }
                }
            }
            catch(UnauthorizedAccessException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (PathTooLongException ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }
    }
}