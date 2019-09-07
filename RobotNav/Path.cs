/*
filename: Path.cs
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
    public class Path
    {
        private Grid _location;

        public Path(Grid location)
        {
            _location = location;
        }

        public Grid Location
        {
            get
            {
                return _location;
            }
            set
            {
                _location = value;
            }
        }
    }
}
