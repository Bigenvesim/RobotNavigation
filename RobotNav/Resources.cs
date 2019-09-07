/*
filename: Resources.cs
author: Pham Phuc Hung Le (101985894)
created: 14/04/2019
last modified: 25/04/2019
description: 
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotNavigation
{
    public class Resources
    {
        private string[] _lines;
        private string _link; 
        private string _data;
        private List<string> _dataList = new List<string>();

        public string Link { get => _link; set => _link = value; }

        // load data from text file and remove all the brackets and commas, then store all the number to a string list
        public List<string> LoadDataFromFile()
        {
            _lines = System.IO.File.ReadAllLines(_link);
            
            int i = 0;
            foreach (string line in _lines)
            {
                if (i == 0)
                {
                    _data = line.Replace(',', ' ').Replace('[', ' ').Replace(']', ' ');
                    _dataList.Add(_data);
                }
                else if (i == 1)
                {
                    _data = line.Replace('(', ' ').Replace(',', ' ').Replace(')', ' ').Replace('|', ' ');
                    _dataList.Add(_data);
                }
                else if (i == 2)
                {
                    _data = line.Replace('(', ' ').Replace(',', ' ').Replace(')', ' ');
                    _dataList.Add(_data);
                }
                else if ((i >= 3))
                {
                    _data = line.Replace('(', ' ').Replace(',', ' ').Replace(')', ' ');
                    _dataList.Add(_data);
                }
                i++;
            }
            return _dataList;
        }
    }
}
