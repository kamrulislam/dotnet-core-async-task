using System;
using System.IO;
using System.Threading.Tasks;

namespace myApp.io {

    class ReadFile {
        public async Task<String> ReadFileAsync(String filename) {
            using (var sr = new StreamReader(filename)) {
                Char[] buffer = new Char[(int)sr.BaseStream.Length];
                await sr.ReadAsync(buffer, 0, (int)sr.BaseStream.Length);
                return new String(buffer);
            }
        }
    } 

}