using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Threads
{
    internal class FileManager
    {
        StreamWriter? _writer;
        private string _path;

        public FileManager(string path)
        {
            _path = path;
        }

        public void writeLineInFile(string data, bool shouldAdd = false)
        {
            _writer = new StreamWriter(_path, shouldAdd);    // If shouldAdd = true, we add a line to the file
            _writer.WriteLine(data);
            _writer.Close();
            _writer.Dispose();
        }
    }
}
