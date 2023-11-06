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

        public void writeLineInFile(string path, string data, bool shouldAdd = false)
        {
            _writer = new StreamWriter(path, shouldAdd);    // If shouldAdd = true, we add a line to the file
            _writer.WriteLine(data);
            _writer.Close();
            _writer.Dispose();
        }

        public void deleteFile(string path)
        {
            FileInfo file = new FileInfo(path);
            if (file.Exists)
            {
                file.Delete();
            }
        }
    }
}
