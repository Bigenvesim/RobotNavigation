/*
filename: Agent.cs
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
    public class Agent
    {
        private int _x;
        private int _y;

        public int X { get => _x; set => _x = value; }
        public int Y { get => _y; set => _y = value; }

        public Agent(Map map)
        {
            _x = map.RobotX;
            _y = map.RobotY;
        }

        public string MoveUp()
        {     
            return "up";
        }

        public string MoveLeft()
        {      
            return "left";
        }

        public string MoveDown()
        {     
            return "down";
        }

        public string MoveRight()
        {
            return "right";
        }
    }
}
