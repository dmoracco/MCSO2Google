using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Scheduler
{
    /// <summary>
    /// FileStream object that also provides list of comma separated lines.
    /// </summary>

    public class CSVFile
    {
        private StreamReader _fileLocation;
        private List<string> _linelist;
        private int _currentline = 1;

        public CSVFile(string path)
        {
            string line;

            _fileLocation = File.OpenText(path);
            while ((line = _fileLocation.ReadLine()) != null)
            {
                _linelist.Add(line);
            }
        }

        public string GetNextLine()
        {
            if (_linelist[_currentline] != null)
                return _linelist[_currentline++];
            else
                return null;
        }
    }
}
    
