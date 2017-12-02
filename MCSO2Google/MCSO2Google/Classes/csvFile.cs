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
        private int _currentline;

        public CSVFile(string path)
        {
            _currentline = 1;
            //add handling for when bad file is passed...
            string line;
            _linelist = new List<string>();

            _fileLocation = File.OpenText(path);
            while ((line = _fileLocation.ReadLine()) != null)
            {
                _linelist.Add(line);
            }
        }

        public string GetNextLine()
        {
            if (_currentline < _linelist.Count)
            {
                string send = _linelist[_currentline];
                _currentline++;
                return send;
            }
                
            else
                return null;
        }
    }
}
    
