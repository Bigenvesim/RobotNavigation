/*
filename: Grid.cs
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
    public class Grid
    {
        private int _x;
        private int _y;
        private string _gridName;
        private Grid _parent;
        private int _sequence;
        private List<Path> _adjacentGrid;
        private double _hCost;
        private double _gCost;
        private double _fCost;
        private double _moveDirectionCost;
        // This move direction is used for GUI
        private string _moveDirection;

        public Grid(Point pt)
        {
            _x = pt.X;
            _y = pt.Y;
            _adjacentGrid = new List<Path>();
        }

        public int X { get => _x; set => _x = value; }
        public int Y { get => _y; set => _y = value; }
        public string GridName { get => _gridName; set => _gridName = value; }
        public List<Path> AdjecentGrids { get => _adjacentGrid; set => _adjacentGrid = value; }
        public Grid ParentGrid { get => _parent; set => _parent = value; }
        public int Sequence { get => _sequence; set => _sequence = value; }
        public double HCost { get => _hCost; set => _hCost = value; }
        public double GCost { get => _gCost; set => _gCost = value; }
        public double FCost { get => _fCost; set => _fCost = value; }
        public string MoveDirection { get => _moveDirection; set => _moveDirection = value; }
        public double MoveDirectionCost { get => _moveDirectionCost; set => _moveDirectionCost = value; }
    }
}
