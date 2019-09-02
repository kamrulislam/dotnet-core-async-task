using System;
using System.IO;
using System.Threading.Tasks;

namespace myApp.io {
    class FileCopier {
        public async Task copy(string filename, string content) {
            using (StreamWriter writer = File.CreateText(filename))
            {
                await writer.WriteAsync(content);
            }
        }
    }
}